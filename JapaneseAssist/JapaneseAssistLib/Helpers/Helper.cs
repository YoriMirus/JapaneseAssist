using System;
using System.Collections.Generic;
using System.Text;

using JapaneseAssistLib.Models;

namespace JapaneseAssistLib.Helpers
{
    internal static class Helper
    {
        /// <summary>
        /// Removes all letters from text that aren't kanji
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FilterNonKanji(string text)
        {
            StringBuilder sb = new StringBuilder(text);

            /* http://www.rikai.com/library/kanjitables/kanji_codes.unicode.shtml
             * Unicode values taken from this site.
             * Chinese characters (kanji) start from the value 17152 and end at 40879
             */
            foreach (char c in text)
            {
                int unicodeValue = (int)c;
                if (unicodeValue < 17152 || unicodeValue > 40879)
                    sb.Replace(c.ToString(), "");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets all of the kanji from the text and compiles them to a list along with how many times they appeared.
        /// </summary>
        /// <param name="kanjiString"></param>
        /// <param name="filterNonKanji">Use this if the text might have characters that aren't kanji.</param>
        /// <returns>List of all found kanji</returns>
        public static List<FoundKanji> GetFoundKanji(string text, bool filterNonKanji)
        {
            List<FoundKanji> foundKanji = new List<FoundKanji>();

            if (filterNonKanji)
                text = FilterNonKanji(text);

            StringBuilder sb = new StringBuilder(text);
            while(sb.Length > 0)
            {
                int prevLength = sb.Length;
                char kanji = sb[0];

                sb.Replace(kanji.ToString(), "");

                foundKanji.Add(new FoundKanji(kanji, prevLength - sb.Length));
            }

            return foundKanji;
        }
    }
}
