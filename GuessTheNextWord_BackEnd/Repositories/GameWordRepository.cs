using GuessTheNextWord_BackEnd.Data;
using GuessTheNextWord_BackEnd.Models;
using GuessTheNextWord_BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GuessTheNextWord_BackEnd.Repositories
{
    public class GameWordRepository : IGameWordRepository
    {
        private readonly AppDbContext _context;
        public GameWordRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool CheckIfWordAlreadyTaken(string word, int gameId)
        {
            var gameWord = _context.GameWords.Where(gw => gw.GameId == gameId).Include(gameWord => gameWord.Word).ToList();
            return gameWord.Any(gw => gw.Word.Text == word);
        }

        public async Task AddWordAsync(GameWord gameWord)
        {
            await _context.GameWords.AddAsync(gameWord);
            await _context.SaveChangesAsync();
        }

        public Word GetLastWordSaidInTheGame(int gameId)
        {
            var gameWords = _context.GameWords.Where(gw => gw.GameId == gameId).Include(gw => gw.Word).ToList();
            return gameWords.OrderByDescending(gw => gw.WordId).First().Word;
        }
    }
}
