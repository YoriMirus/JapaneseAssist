using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using JapaneseAssistLib.Events;
using JapaneseAssistLib.Helpers;
using JapaneseAssistLib.Models;

namespace JapaneseAssistLib
{
    public static class TextAnalyzer
    {
        public static event InputTextChanged InputTextChanged;

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

        public static ObservableCollection<char> IgnoredKanji { get; private set; }

        private static void Start()
        {
            string text = InputText;
            List<FoundKanji> foundKanji = Helper.GetFoundKanji(text, true);
            foundKanji.RemoveAll(x => IgnoredKanji.Contains(x.Kanji));

            InputTextChanged?.Invoke(new InputTextChangedEventArgs(InputText, foundKanji));
        }

        /// <summary>
        /// Initializes the text analyzer (e.g. loads ignored kanji from the database etc.)
        /// </summary>
        public static void Initialize()
        {
            //No database is implemented, so nothing is here yet. Only instantiating IgnoredKanji to prevent a null exception.
            IgnoredKanji = new ObservableCollection<char>();
        }
    }
}
