using GuessTheNextWord_BackEnd.Data;
using GuessTheNextWord_BackEnd.Models;
using GuessTheNextWord_BackEnd.Repositories.Interfaces;

namespace GuessTheNextWord_BackEnd.Repositories
{
    public class GamePlayerRepository : IGamePlayersRepository
    {
        private readonly AppDbContext _dbContext;
        public GamePlayerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddGamePlayerAsync(GamePlayer gamePlayer)
        {
            await _dbContext.GamePlayers.AddAsync(gamePlayer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddGamePlayersAsync(List<GamePlayer> gamePlayers)
        {
            await _dbContext.GamePlayers.AddRangeAsync(gamePlayers);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<GamePlayer> GetGamePlayerAsync(int gameId, int playerId)
        {
            return await _dbContext.GamePlayers.FindAsync(gameId, playerId);
        }

        public async Task RemoveGamePlayerAsync(int gameId, int playerId)
        {
            var gamePlayer = await GetGamePlayerAsync(gameId, playerId);
            if (gamePlayer != null)
            {
                _dbContext.GamePlayers.Remove(gamePlayer);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
