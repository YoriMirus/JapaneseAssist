using System;
using System.Windows;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Text;

namespace JapaneseAssist.ViewModels
{
    class WordsAnalysisViewModel : ViewModelBase
    {
        public readonly FlowDocument WordInformationDocument;
        private string _InputText;
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
