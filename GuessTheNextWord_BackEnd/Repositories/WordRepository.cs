using GuessTheNextWord_BackEnd.Data;
using GuessTheNextWord_BackEnd.Models;
using GuessTheNextWord_BackEnd.Repositories.Interfaces;

namespace GuessTheNextWord_BackEnd.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly AppDbContext _context;
        public WordRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckIfWordAlreadyTaken(string word)
        {
            return await Task.FromResult(_context.Words.Any(w => w.Text == word));
        }

        public async Task AddWordAsync(string word)
        {
            await _context.Words.AddAsync(new Word(){Text = word});
            await _context.SaveChangesAsync();
        }
    }
}
