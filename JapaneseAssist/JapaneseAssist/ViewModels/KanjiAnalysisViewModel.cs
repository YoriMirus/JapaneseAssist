using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Documents;

using JapaneseAssist.Models;
using JapaneseAssistLib;
using JapaneseAssistLib.API;
using JapaneseAssistLib.Events;
using JapaneseAssistLib.Models;

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

                //There is no need for await
                if(value > 0)
                    DisplayKanjiInfo(IgnoredKanji[value]);
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

                //There is no need for await
                if(value > 0)
                    DisplayKanjiInfo(FoundKanji[value].Kanji);
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

            IgnoredKanji = TextAnalyzer.IgnoredKanji;
            FoundKanji = new ObservableCollection<FoundKanji>();

            TextAnalyzer.OutputChanged += OnInputTextChanged;
        }
        
        /// <summary>
        /// Adds an ignored kanji from the FoundKanji list.
        /// Ignores duplicate kanji and handles Exceptions like index being -1
        /// </summary>
        /// <param name="index"></param>
        private async Task AddIgnoredKanji(int index)
        {
            if (index > -1)
            {
                FoundKanji fk = this.FoundKanji[index];
                await TextAnalyzer.AddIgnoredKanji(fk.Kanji);
            }
        }
        /// <summary>
        /// Removes ignored kanji from the IgnoredKanji list.
        /// Handles exceptions like index being -1
        /// </summary>
        /// <param name="index"></param>
        private async Task RemoveIgnoredKanji(int index)
        {
            if (index > -1)
            {
                await TextAnalyzer.RemoveIgnoredKanji(IgnoredKanji[index]);
            }
        }

        private void OnInputTextChanged(TextAnalysisOutputChangedEventArgs args)
        {
            FoundKanji.Clear();
            foreach(FoundKanji fk in args.FoundKanji)
            {
                FoundKanji.Add(fk);
            }
        }
        private async Task DisplayKanjiInfo(char kanji)
        {
            //Get entry
            KanjiAPIEntry entry = await KanjiAPI.GetKanjiInfoAsync(kanji);

            //Remove previous entry from the document
            KanjiInformationDocument.Blocks.Clear();

            //Display character
            Paragraph p = new Paragraph();
            p.TextAlignment = System.Windows.TextAlignment.Center;

            p.Inlines.Add(new Run()
            {
                Text = entry.Kanji,
                FontSize = 30,
                FontWeight = System.Windows.FontWeights.Bold
            });
            KanjiInformationDocument.Blocks.Add(p);


            //Display kanji meanings
            p = new Paragraph();
            p.TextAlignment = System.Windows.TextAlignment.Left;

            p.Inlines.Add(new Run()
            {
                Text = "Meaning: " + entry.GetMeanings() + "\n",
                FontSize = 16,
            });


            //Display kanji kunyomi readings
            p.Inlines.Add(new Run()
            {
                Text = "Kunyomi: " + entry.GetKunyomi() + "\n",
                FontSize = 16,
            });
            KanjiInformationDocument.Blocks.Add(p);


            //Display kanji onyomi readings
            p.Inlines.Add(new Run()
            {
                Text = "Onyomi: " + entry.GetOnyomi() + "\n",
                FontSize = 16,
            });
            KanjiInformationDocument.Blocks.Add(p);


            //Display JLPT
            p.Inlines.Add(new Run()
            {
                Text = "JLPT: N" + entry.Jlpt,
                FontSize = 16,
            });
            KanjiInformationDocument.Blocks.Add(p);


        }
    }
}
