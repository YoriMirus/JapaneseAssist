using JapaneseAssist.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JapaneseAssist.ViewModels
{
    class KanjiAnalysisViewModel: ViewModelBase
    {
        private ObservableCollection<FoundKanji> _FoundKanji;
        public ObservableCollection<FoundKanji> FoundKanji
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

        private ObservableCollection<char> _IgnoredKanji;
        public ObservableCollection<char> IgnoredKanji
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

        private int _IgnoredKanjiIndex;
        public int IgnoredKanjiIndex
        {
            get
            {
                return _IgnoredKanjiIndex;
            }
            set
            {
                _IgnoredKanjiIndex = value;
                OnPropertyChanged();
                RemoveIgnoredKanjiCommand.FireCanExecuteChanged();
            }
        }

        private int _FoundKanjiIndex;
        public int FoundKanjiIndex
        {
            get
            {
                return _FoundKanjiIndex;
            }
            set
            {
                _FoundKanjiIndex = value;
                OnPropertyChanged();
                AddIgnoredKanjiCommand.FireCanExecuteChanged();
            }
        }

        private ButtonCommand _AddIgnoredKanjiCommand;
        public ButtonCommand AddIgnoredKanjiCommand
        {
            get
            {
                return _AddIgnoredKanjiCommand;
            }
            set
            {
                _AddIgnoredKanjiCommand = value;
                OnPropertyChanged();
            }
        }

        private ButtonCommand _RemoveIgnoredKanjiCommand;
        public ButtonCommand RemoveIgnoredKanjiCommand
        {
            get
            {
                return _RemoveIgnoredKanjiCommand;
            }
            set
            {
                _RemoveIgnoredKanjiCommand = value;
                OnPropertyChanged();
            }
        }

        public KanjiAnalysisViewModel()
        {
            AddIgnoredKanjiCommand = new ButtonCommand(() => AddIgnoredKanji(FoundKanjiIndex), () => FoundKanjiIndex != -1);
            RemoveIgnoredKanjiCommand = new ButtonCommand(() => RemoveIgnoredKanji(IgnoredKanjiIndex), () => IgnoredKanjiIndex != -1);

            FoundKanji = new ObservableCollection<FoundKanji>
            {
                new FoundKanji('休', 5),
                new FoundKanji('運', 3)
            };
            IgnoredKanji = new ObservableCollection<char>
            {
                '日'
            };
        }
        
        /// <summary>
        /// Adds an ignored kanji from the FoundKanji list.
        /// </summary>
        /// <param name="index"></param>
        private void AddIgnoredKanji(int index)
        {
            FoundKanji fk = this.FoundKanji[index];

            if (!IgnoredKanji.Contains(fk.Kanji))
            {
                IgnoredKanji.Add(fk.Kanji);
                FoundKanji.RemoveAt(index);
            }


            OnPropertyChanged("IgnoredKanji");
            OnPropertyChanged("FoundKanji");
            //Ignored kanji should be added to a database, but this will be enough for now.
        }

        private void RemoveIgnoredKanji(int index)
        {
            IgnoredKanji.RemoveAt(index);

            OnPropertyChanged("IgnoredKanji");
            OnPropertyChanged("FoundKanji");
            //Ignored kanji should be removed from the database, but this will be enough for now.
        }
    }
}
