using GuessTheNextWord_BackEnd.DTOs.PlayerDTOs;
using GuessTheNextWord_BackEnd.Models;
using GuessTheNextWord_BackEnd.Models.Enums;
using GuessTheNextWord_BackEnd.Repositories.Interfaces;
using GuessTheNextWord_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessTheNextWord_BackEnd.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameWordRepository _gameWordRepository;
        private readonly IGamePlayersRepository _gamePlayersRepository;
        public PlayerController(IGameRepository gameRepository, IPlayerRepository playerRepository, IGameWordRepository gameWordRepository, IGamePlayersRepository gamePlayersRepository, WordLookUp wordLookUp)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _gameWordRepository = gameWordRepository;
            _gamePlayersRepository = gamePlayersRepository;
            WordLookUp.LoadWords(
                @"D:\GuessTheNextWord\GuessTheNextWord_BackEnd\GuessTheNextWord_BackEnd\bin\Debug\net8.0\words_alpha.txt");
        }
        [HttpGet("button-pressed")]
        public async Task<IActionResult> ButtonPressed(ButtonPressedPlayerRequest bpRequest)
        {
            var player = await _playerRepository.GetPlayerByIdAsync(bpRequest.PlayerId);

            if (player == null)
            {
                return NotFound("Player not found");
            }

            var game = await _gameRepository.GetGameByIdAsync(bpRequest.GameId);

            if (game == null)
            {
                return NotFound("Game not found");
            }

            var gameId = game.Id;

            if (bpRequest.SecondsLimit > 30)
            {
                await _playerRepository.UpdatePlayerStateByIdAsync(player.Id, PlayerState.Lost);
                if (await _gameRepository.IsOnePlayerLeft(gameId))
                {
                    return RedirectToAction("EndGame", "Game",new {gameId = gameId});
                }
                return BadRequest("You took too long to answer, you are disqualified from this game");
            }

            var bpRequestWord = bpRequest.Word;

            if (string.IsNullOrEmpty(bpRequestWord))
            {
                return BadRequest("Empty word");
            }

            if (!WordLookUp.IsValidWord(bpRequestWord))
            {
                return BadRequest("Word does not exist");
            }

            if (_gameRepository.HasGameAnyWords(gameId))
            {
                if (_gameWordRepository.GetLastWordSaidInTheGame(gameId).Text[^1] != bpRequestWord[0])
                {
                    return BadRequest("The current word should start with the ending character of the previous word");
                }

                if (_gameWordRepository.CheckIfWordAlreadyTaken(bpRequestWord, gameId))
                {
                    return BadRequest("WordAlreadyTaken");
                }
            }

            var gameWord = new GameWord
            {
                Game = game,
                Word = new Word { Text = bpRequestWord }
            };
            await _gameWordRepository.AddWordAsync(gameWord);

            return Ok();
        }
        [HttpPost("leave-game")]
        public async Task<IActionResult> LeaveGame(LeaveGameRequest lgRequest)
        {
            var player = await _playerRepository.GetPlayerByIdAsync(lgRequest.PlayerId);
            if (player == null)
            {
                return NotFound("Player not found");
            }
            var game = await _gameRepository.GetGameByIdAsync(lgRequest.GameId);
            if (game == null)
            {
                return NotFound("Game not found");
            }
            await _playerRepository.UpdatePlayerStateByIdAsync(player.Id, PlayerState.Left);
            if (await _gameRepository.IsOnePlayerLeft(game.Id))
            {
                return RedirectToAction("EndGame", "Game", new { gameId = game.Id });
            }
            return Ok();
        }
    }
}
