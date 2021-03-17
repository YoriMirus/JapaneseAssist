using System;
using WK.Libraries.SharpClipboardNS;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Text;

using JapaneseAssistLib.Events;
using JapaneseAssistLib.Helpers;
using JapaneseAssistLib.Models;
using JapaneseAssistDB;

namespace JapaneseAssistLib
{
    public static class TextAnalyzer
    {
        public static event TextAnalysisOutputChanged OutputChanged;
        public static readonly ObservableCollection<char> IgnoredKanji = new ObservableCollection<char>();
        public static readonly SharpClipboard ClipboardMonitorer = new SharpClipboard();

        private static string _InputText;
        /// <summary>
        /// Japanese text that is being analyzed. Automatically starts proessing the text once a value is assigned, which might take some time.
        /// </summary>
        public static string InputText
        {
            get
            {
                return _InputText;
            }
            set
            {
                _InputText = value;
                Start();
            }
        }



        private static void Start()
        {
            string text = InputText;
            List<FoundKanji> foundKanji = Helper.GetFoundKanji(text, true);
            foundKanji.RemoveAll(x => IgnoredKanji.Contains(x.Kanji));

            foundKanji = foundKanji.OrderByDescending(x => x.Appeared).ToList();

            OutputChanged?.Invoke(new TextAnalysisOutputChangedEventArgs(InputText, foundKanji));
        }

        /// <summary>
        /// Adds an ignored kanji to the list and then to the database.
        /// </summary>
        /// <param name="kanji"></param>
        /// <remarks>This is the way ignored kanji should be added, otherwise you have to both add the kanji to the obserable collection and to the database yourself.</remarks>
        public static async Task AddIgnoredKanji(char kanji)
        {
            IgnoredKanji.Add(kanji);
            await DBAccess.AddIgnoredKanjiAsync(kanji);
            Start();
        }

        public static async Task RemoveIgnoredKanji(char kanji)
        {
            IgnoredKanji.Remove(kanji);
            await DBAccess.RemoveIgnoredKanjiAsync(kanji);
            Start();
        }

        /// <summary>
        /// Initializes the text analyzer (e.g. loads ignored kanji from the database etc.)
        /// </summary>
        public static void Initialize()
        {
            IEnumerable<char> ignoredKanji = DBAccess.GetIgnoredKanji();
            IgnoredKanji.Clear();
            
            foreach(char c in ignoredKanji)
            {
                IgnoredKanji.Add(c);
            }
        }
        /// <summary>
        /// Initializes the text analyzer (e.g. loads ignored kanji from the database etc.)
        /// </summary>
        public static async Task InitializeAsync()
        {
            IEnumerable<char> ignoredKanji = await DBAccess.GetIgnoredKanjiAsync();
            IgnoredKanji.Clear();

            foreach (char c in ignoredKanji)
            {
                IgnoredKanji.Add(c);
            }
        }
    }
}
