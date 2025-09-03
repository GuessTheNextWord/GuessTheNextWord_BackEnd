using System.ComponentModel.DataAnnotations;
using GuessTheNextWord_BackEnd.Models.Enums;

namespace GuessTheNextWord_BackEnd.Models
{
    public class Player
    {
        public Player()
        {
            Games = new List<GamePlayer>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public PlayerState State { get; set; }

        public ICollection<GamePlayer> Games { get; set; }
    }
}
