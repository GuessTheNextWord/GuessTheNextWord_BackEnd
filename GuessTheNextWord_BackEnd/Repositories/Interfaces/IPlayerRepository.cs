using GuessTheNextWord_BackEnd.Models;
using GuessTheNextWord_BackEnd.Models.Enums;

namespace GuessTheNextWord_BackEnd.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        Task AddPlayerAsync(Player player);
        Task<Player> GetPlayerByIdAsync(int playerId);
        Task UpdatePlayerStateByIdAsync(int playerId, PlayerState state);
    }
}
