using System.ComponentModel.DataAnnotations;
using GuessTheNextWord_BackEnd.Models.Enums;

namespace GuessTheNextWord_BackEnd.Models
{
    public class Game
    {
        public Game()
        {
            GamePlayers = new List<GamePlayer>();
            Words = new List<GameWord>();
        }
        [Key]
        public int Id { get; set; }
        public GameState State { get; set; }
        public ICollection<GamePlayer> GamePlayers { get; set; } 
        public ICollection<GameWord> Words { get; set; }
    }
}
