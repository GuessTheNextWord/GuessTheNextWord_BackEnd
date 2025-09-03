using System.ComponentModel.DataAnnotations.Schema;

namespace GuessTheNextWord_BackEnd.Models
{
    public class GameWord
    {
        public int GameId { get; set; }
        [ForeignKey(nameof(GameId))] 
        public Game Game { get; set; } = null!;
        public int WordId { get; set; }
        [ForeignKey(nameof(WordId))]
        public Word Word { get; set; } = null!;
    }
}
