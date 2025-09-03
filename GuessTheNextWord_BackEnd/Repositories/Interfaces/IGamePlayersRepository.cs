using GuessTheNextWord_BackEnd.Models;

namespace GuessTheNextWord_BackEnd.Repositories.Interfaces
{
    public interface IGamePlayersRepository
    {
        Task AddGamePlayerAsync(GamePlayer gamePlayer);
        Task AddGamePlayersAsync(List<GamePlayer> gamePlayers);
        Task<GamePlayer> GetGamePlayerAsync(int gameId, int playerId);
        Task RemoveGamePlayerAsync(int gameId, int playerId);
    }
}
