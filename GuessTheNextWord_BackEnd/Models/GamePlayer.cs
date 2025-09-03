using System.ComponentModel.DataAnnotations.Schema;

namespace GuessTheNextWord_BackEnd.Models
{
    public class GamePlayer
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; } = null!;

        [ForeignKey(nameof(PlayerId))]

        public Player Player { get; set; } = null;
    }
}
