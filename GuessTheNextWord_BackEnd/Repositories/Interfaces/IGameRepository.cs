using GuessTheNextWord_BackEnd.Models;

namespace GuessTheNextWord_BackEnd.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task AddGameAsync(Game game);
        Task<Game> GetGameByIdAsync(int gameId);
        Task<bool> IsOnePlayerLeft(int gameId);
        Task UpdateGameStateByIdAsync(int gameId, Models.Enums.GameState state);

    }
}
