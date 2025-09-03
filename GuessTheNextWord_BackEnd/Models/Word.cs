using System.ComponentModel.DataAnnotations;

namespace GuessTheNextWord_BackEnd.Models
{
    public class Word
    {
        public Word()
        {
            Games = new List<GameWord>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; } = string.Empty;
        public ICollection<GameWord> Games { get; set; }
    }
}
