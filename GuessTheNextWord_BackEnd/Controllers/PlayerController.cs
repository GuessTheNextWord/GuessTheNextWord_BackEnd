using GuessTheNextWord_BackEnd.DTOs.PlayerDTOs;
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
        private readonly IWordRepository _wordRepository;
        private readonly IGamePlayersRepository _gamePlayersRepository;
        public PlayerController(IGameRepository gameRepository, IPlayerRepository playerRepository, IWordRepository wordRepository, IGamePlayersRepository gamePlayersRepository, WordLookUp wordLookUp)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _wordRepository = wordRepository;
            _gamePlayersRepository = gamePlayersRepository;
            
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

            if (bpRequest.SecondsLimit > 30)
            {
                await _playerRepository.UpdatePlayerStateByIdAsync(player.Id, PlayerState.Lost);
                if (await _gameRepository.IsOnePlayerLeft(game.Id))
                {
                    return RedirectToAction("EndGame", "Game",new {gameId = game.Id});
                }
                return BadRequest("You took too long to answer, you are disqualified from this game");
            }
    
            if (string.IsNullOrEmpty(bpRequest.Word))
            {
                return BadRequest("Empty word");
            }

           

            if (_wordRepository.CheckIfWordAlreadyTaken(bpRequest.Word).Result)
            {
                return BadRequest("WordAlreadyTaken");
            }
            else
            {
                if (!WordLookUp.IsValidWord(bpRequest.Word))
                {
                    return BadRequest("Word does not exist");
                }
                else
                {
                    await _wordRepository.AddWordAsync(bpRequest.Word);
                }
            }

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
