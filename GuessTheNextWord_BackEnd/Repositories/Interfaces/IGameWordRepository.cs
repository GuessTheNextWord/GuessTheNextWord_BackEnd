using GuessTheNextWord_BackEnd.Models;

namespace GuessTheNextWord_BackEnd.Repositories.Interfaces
{
    public interface IGameWordRepository
    {
        bool CheckIfWordAlreadyTaken(string word,int gameId);
        Task AddWordAsync(GameWord gameWord);
        Word GetLastWordSaidInTheGame(int gameId);
    }
}
