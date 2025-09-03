namespace GuessTheNextWord_BackEnd.Repositories.Interfaces
{
    public interface IWordRepository
    {
        Task<bool> CheckIfWordAlreadyTaken(string word);
        Task AddWordAsync(string word);
    }
}
