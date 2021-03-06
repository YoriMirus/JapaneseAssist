namespace JapaneseAssistLib.Models
{
    public class FoundKanji
    {
        /// <summary>
        /// Kanji that was found
        /// </summary>
        public char Kanji { get; private set; }
        /// <summary>
        /// The amount of time it appeared in text
        /// </summary>
        public int Appeared { get; private set; }

        public FoundKanji(char kanji, int appeared)
        {
            Kanji = kanji;
            Appeared = appeared;
        }
    }
}
