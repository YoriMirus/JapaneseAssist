using System;
using System.Collections.Generic;
using System.Text;

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

        public DictionariesViewModel()
        {
            KanjiInput = "Test kanji.";
            WordInput = "Test word.";
        }
    }
}
