using JapaneseAssist.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Documents;

namespace JapaneseAssist.ViewModels
{
    class KanjiAnalysisViewModel: ViewModelBase
    {
        private ObservableCollection<FoundKanji> _FoundKanji;
        /// <summary>
        /// Kanji that has been found in the inputted text
        /// </summary>
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
        /// <summary>
        /// Kanji that won't appear in the FoundKanji list
        /// </summary>
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
        /// <summary>
        /// Which ignored kanji is selected by the user
        /// </summary>
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
        /// <summary>
        /// Which found kanji is selected by the user
        /// </summary>
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
        /// <summary>
        /// A ButtonCommand that adds kanji from the FoundKanji that the user selected
        /// </summary>
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
        /// <summary>
        /// A ButtonCommand that removes ignored kanji selected by the user.
        /// </summary>
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

        //This will be implemented later once I have an SQLite database storing a kanji dictionary
        /// <summary>
        /// Document that contains information about a kanji readable by the user.
        /// </summary>
        public readonly FlowDocument KanjiInformationDocument;

        public KanjiAnalysisViewModel()
        {
            AddIgnoredKanjiCommand = new ButtonCommand(() => AddIgnoredKanji(FoundKanjiIndex), () => FoundKanjiIndex != -1);
            RemoveIgnoredKanjiCommand = new ButtonCommand(() => RemoveIgnoredKanji(IgnoredKanjiIndex), () => IgnoredKanjiIndex != -1);
            KanjiInformationDocument = new FlowDocument();

            Paragraph p = new Paragraph();
            p.Inlines.Add(new Run()
            {
                Text = "Test",
                FontWeight = System.Windows.FontWeights.Bold,
                Foreground = System.Windows.Media.Brushes.Red,
            });
            p.TextAlignment = System.Windows.TextAlignment.Center;

            KanjiInformationDocument.Blocks.Add(p);

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
        /// Ignores duplicate kanji and handles Exceptions like index being -1
        /// </summary>
        /// <param name="index"></param>
        private void AddIgnoredKanji(int index)
        {
            if (index > -1)
            {
                FoundKanji fk = this.FoundKanji[index];

                if (!IgnoredKanji.Contains(fk.Kanji))
                {
                    IgnoredKanji.Add(fk.Kanji);
                    FoundKanji.RemoveAt(index);
                }


                OnPropertyChanged("IgnoredKanji");
                OnPropertyChanged("FoundKanji");
            }
            //Ignored kanji should be added to a database, but this will be enough for now.
        }
        /// <summary>
        /// Removes ignored kanji from the IgnoredKanji list.
        /// Handles exceptions like index being -1
        /// </summary>
        /// <param name="index"></param>
        private void RemoveIgnoredKanji(int index)
        {
            if (index > -1)
            {
                IgnoredKanji.RemoveAt(index);

                OnPropertyChanged("IgnoredKanji");
                OnPropertyChanged("FoundKanji");
            }
            //Ignored kanji should be removed from the database, but this will be enough for now.
        }
    }
}
