using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Collections.Generic;

using JapaneseAssist.Helpers;

using JapaneseAssistLib;
using JapaneseAssistLib.Events;
using JapaneseAssistLib.API;
using JapaneseAssistLib.Models;

namespace JapaneseAssist.ViewModels
{
    class WordsLookupViewModel : ViewModelBase
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

        private string _SelectedText;
        public string SelectedText
        {
            get
            {
                return _SelectedText;
            }
            set
            {
                if(_SelectedText != value)
                {
                    _SelectedText = value;
                    _ = WaitAndGetEntry();
                }
            }
        }

        public WordsLookupViewModel()
        {
            WordInformationDocument = new FlowDocument();
            TextAnalyzer.OutputChanged += OnTextAnalyzerOutputChanged;
        }

        private async Task WaitAndGetEntry()
        {
            string helper = SelectedText;
            await Task.Run(() => Thread.Sleep(750));
            if (helper == SelectedText && !string.IsNullOrEmpty(SelectedText))
            {
                //Notify the user that the search has started
                WordInformationDocument.Blocks.Clear();
                Paragraph p = new Paragraph(new Run()
                {
                    Text = "Searching...",
                    FontSize = 22,
                    FontWeight = FontWeights.Bold
                });
                p.TextAlignment = TextAlignment.Center;
                WordInformationDocument.Blocks.Add(p);

                //Search for an entry
                List<JishoEntry> entries = await JishoAPI.GetJishoEntry(SelectedText);
                ApiToDocumentHelper.WriteJishoToDocument(entries, WordInformationDocument, true);
            }
        }

        void OnTextAnalyzerOutputChanged(TextAnalysisOutputChangedEventArgs eventArgs)
        {
            InputText = eventArgs.NewText;
        }
    }
}
