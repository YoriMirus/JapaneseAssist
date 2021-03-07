using System;
using System.Windows;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Text;

namespace JapaneseAssist.ViewModels
{
    class WordsAnalysisViewModel : ViewModelBase
    {
        //This will be implemented later once I have an SQLite database with a Japanese word dictionary.
        /// <summary>
        /// Information about a specific word in a format readable by the user
        /// </summary>
        public readonly FlowDocument WordInformationDocument;
        private string _InputText;
        /// <summary>
        /// Japanese text inputted by the user
        /// </summary>
        public string InputText
        {
            get
            {
                return _InputText;
            }
            set
            {
                _InputText = value;
                OnPropertyChanged();
            }
        }

        public WordsAnalysisViewModel()
        {
            WordInformationDocument = new FlowDocument();

            Paragraph p = new Paragraph();
            p.Inlines.Add(new Run()
            {
                FontSize = 18,
                Foreground = System.Windows.Media.Brushes.Red,
                Text = "Test"
            });
            p.TextAlignment = TextAlignment.Center;

            WordInformationDocument.Blocks.Add(p);

            InputText = "Test text.";
        }
    }
}
