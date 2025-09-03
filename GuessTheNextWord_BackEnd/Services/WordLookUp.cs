namespace GuessTheNextWord_BackEnd.Services
{
    public class WordLookUp 
    {
        public WordLookUp()
        {
            Words = new HashSet<string>();
        }
        public static HashSet<string> Words { get; private set; }

        public static void LoadWords(string path)
        {
            Words =
            [
                ..File.ReadLines(path).Select(line => line.Trim().ToLower())
            ];
        }

        public static bool IsValidWord(string word)
        {
            return Words.Contains(word.ToLower());
        }
    }
}
