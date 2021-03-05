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

        public readonly FlowDocument KanjiEntry;
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
