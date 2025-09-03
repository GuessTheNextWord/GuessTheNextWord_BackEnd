namespace GuessTheNextWord_BackEnd.DTOs.PlayerDTOs
{
    public class ButtonPressedPlayerRequest
    {
        public string Word { get; set; } = string.Empty;
        public int PlayerId { get; set; }
        public int GameId { get; set; }

        public int SecondsLimit { get; set; }
    }
}
