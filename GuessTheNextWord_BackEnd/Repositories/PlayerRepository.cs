using GuessTheNextWord_BackEnd.Data;
using GuessTheNextWord_BackEnd.Models;
using GuessTheNextWord_BackEnd.Models.Enums;
using GuessTheNextWord_BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GuessTheNextWord_BackEnd.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _dbContext;
        public PlayerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddPlayerAsync(Player player)
        {
            await _dbContext.Players.AddAsync(player);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Player> GetPlayerByIdAsync(int playerId)
        {
            return await _dbContext.Players.FindAsync(playerId);
        }

        public async Task UpdatePlayerStateByIdAsync(int playerId, PlayerState state)
        {
            var player = GetPlayerByIdAsync(playerId).Result;
            player.State = state;
            await _dbContext.SaveChangesAsync();
        }
    }
}
