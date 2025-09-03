using GuessTheNextWord_BackEnd.DTOs.GameDTOs;
using GuessTheNextWord_BackEnd.Models;
using GuessTheNextWord_BackEnd.Models.Enums;
using GuessTheNextWord_BackEnd.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GuessTheNextWord_BackEnd.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGamePlayersRepository _gamePlayersRepository;
        public GameController(IGameRepository gameRepository, IPlayerRepository playerRepository, IGamePlayersRepository gamePlayersRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _gamePlayersRepository = gamePlayersRepository;
        }
        [HttpPost("initialize-game")]
        public async Task<IActionResult> InitializeGame([FromBody] InitializeGameRequest request)
        {
            var game = new Game { State = GameState.InProgress };

            for (int i = 1; i <= request.PlayersCount; i++)
            {
                var player = new Player { Name = $"Player{i}", State = PlayerState.Playing };
                var gamePlayer = new GamePlayer { Game = game, Player = player };

                game.GamePlayers.Add(gamePlayer);

                player.Games.Add(gamePlayer);

               await _playerRepository.AddPlayerAsync(player);

            }
            return Ok();
        }
        [HttpPost("game-end")]
        public async Task<IActionResult> EndGame(int gameId)
        {
            var game = await _gameRepository.GetGameByIdAsync(gameId);

            if (game == null)
            {
                return NotFound("Game not found");
            }

            await _gameRepository.UpdateGameStateByIdAsync(game.Id, GameState.Ended);
            
            var playerWon = game.GamePlayers.FirstOrDefault(gp => gp.Player.State == PlayerState.Playing)?.Player;
            if (playerWon != null)
            {
                await _playerRepository.UpdatePlayerStateByIdAsync(playerWon.Id, PlayerState.Won);
            }
            return Ok();
        }
        [HttpPost("stop-game")]
        public async Task<IActionResult> StopGame(StopGameRequest gameRequest)
        {
            var game = await _gameRepository.GetGameByIdAsync(gameRequest.GameId);
            if (game == null)
            {
                return NotFound("Game not found");
            }
            await _gameRepository.UpdateGameStateByIdAsync(game.Id, GameState.Ended);
            foreach (var gp in game.GamePlayers)
            {
                await _playerRepository.UpdatePlayerStateByIdAsync(gp.PlayerId, PlayerState.Lost);
            }
            return Ok();
        }
    }
}
