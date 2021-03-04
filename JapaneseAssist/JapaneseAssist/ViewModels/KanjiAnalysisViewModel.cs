using System;
using System.Collections.Generic;
using System.Text;

using JapaneseAssist.Models;

namespace JapaneseAssist.ViewModels
{
    class KanjiAnalysisViewModel: ViewModelBase
    {
        private List<FoundKanji> _FoundKanji;
        public List<FoundKanji> FoundKanji
        {
            get
            {
                return _FoundKanji;
            }
            set
            {
                _FoundKanji = value;
                OnPropertyChanged();
            }
        }

        private List<char> _IgnoredKanji;
        public List<char> IgnoredKanji
        {
            get
            {
                return _IgnoredKanji;
            }
            set
            {
                _IgnoredKanji = value;
                OnPropertyChanged();
            }
        }

        public KanjiAnalysisViewModel()
        {
            FoundKanji = new List<FoundKanji>
            {
                new FoundKanji('休', 5)
            };
            IgnoredKanji = new List<char>
            {
                '日'
            };
        }
    }
}
