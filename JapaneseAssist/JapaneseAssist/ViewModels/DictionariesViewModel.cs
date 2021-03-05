using System;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Text;

using JapaneseAssist.Models;

namespace JapaneseAssist.ViewModels
{
    class DictionariesViewModel : ViewModelBase
    {
        private string _WordInput;
        /// <summary>
        /// A word that the user inputted
        /// </summary>
        public string WordInput
        {
            get
            {
                return _WordInput;
            }
            set
            {
                _WordInput = value;
                OnPropertyChanged();
            }
        }

        private string _KanjiInput;
        /// <summary>
        /// Kanji that the user inputted
        /// </summary>
        public string KanjiInput
        {
            get
            {
                return _KanjiInput;
            }
            set
            {
                _KanjiInput = value;
                OnPropertyChanged();
            }
        }

        private ButtonCommand _SearchWordButton;
        /// <summary>
        /// Search button for the kanji dictionary
        /// </summary>
        public ButtonCommand SearchWordButton
        {
            get
            {
                return _SearchWordButton;
            }
            set
            {
                _SearchWordButton = value;
                OnPropertyChanged();
            }
        }

        private ButtonCommand _SearchKanjiButton;
        /// <summary>
        /// Search button for the Japanese word dictionary
        /// </summary>
        public ButtonCommand SearchKanjiButton
        {
            get
            {
                return _SearchKanjiButton;
            }
            set
            {
                _SearchKanjiButton = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A kanji search result in a format readable by the user.
        /// </summary>
        public readonly FlowDocument KanjiEntry;
        /// <summary>
        /// A word search result in a format readable by the user.
        /// </summary>
        public readonly FlowDocument WordEntry;

        private void SearchKanji()
        {
            //Do some searching stuff
        }

        private void SearchWord()
        {
            //Do some searching stuff
        }

        public DictionariesViewModel()
        {
            KanjiInput = "Test kanji.";
            WordInput = "Test word.";

            KanjiEntry = new FlowDocument();
            WordEntry = new FlowDocument();

            SearchKanjiButton = new ButtonCommand(SearchKanji, () => KanjiInput.Length > 0);
            SearchWordButton = new ButtonCommand(SearchWord, () => WordInput.Length > 0);
        }
    }
}
