using GuessTheNextWord_BackEnd.Data;
using GuessTheNextWord_BackEnd.Models;
using GuessTheNextWord_BackEnd.Models.Enums;
using GuessTheNextWord_BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GuessTheNextWord_BackEnd.Repositories
{
    public class GameRepository : IGameRepository
    {
        private AppDbContext dbContext;
        public GameRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddGameAsync(Game game)
        {
            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Game> GetGameByIdAsync(int gameId)
        {
            return await dbContext.Games.FindAsync(gameId);
        }

        public async Task<bool> IsOnePlayerLeft(int gameId)
        {
            return await dbContext.GamePlayers.CountAsync(gp => gp.GameId == gameId && gp.Player.State == Models.Enums.PlayerState.Playing) == 1;
        }

        public Task UpdateGameStateByIdAsync(int gameId, GameState state)
        {
            var game = GetGameByIdAsync(gameId).Result;
            game.State = state;
            return dbContext.SaveChangesAsync();
        }

        public bool HasGameAnyWords(int gameId)
        {
            return dbContext.Games.Include(g => g.Words).Any(g => g.Id == gameId && g.Words.Any());
        }
    }
}
