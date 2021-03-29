using System;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Text;

using JapaneseAssist.Helpers;

using JapaneseAssistLib;
using JapaneseAssistLib.API;
using JapaneseAssistLib.Events;
using JapaneseAssistLib.Models;
using JapaneseAssistLib.Helpers;

namespace JapaneseAssist.ViewModels
{
    class KanjiLookupViewModel: ViewModelBase
    {
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

        private string _SelectedText;
        public string SelectedText
        {
            get
            {
                return _SelectedText;
            }
            set
            {
                if (_SelectedText != value)
                {
                    _SelectedText = value;
                    OnPropertyChanged();
                    _ = WaitAndGetEntry();
                }
            }
        }

        public readonly FlowDocument KanjiInformationDocument;

        public KanjiLookupViewModel()
        {
            KanjiInformationDocument = new FlowDocument();
            TextAnalyzer.OutputChanged += OnTextAnalysisOutputChanged;
        }

        private async Task WaitAndGetEntry()
        {
            //Wait for a bit to confirm that the user selected the text they wanted
            string helper = SelectedText;
            await Task.Run(() => Thread.Sleep(400));

            //No need to start when there is nothing to search
            if (helper == SelectedText && !string.IsNullOrEmpty(SelectedText))
            {
                //Notify the user that a search has started
                KanjiInformationDocument.Blocks.Clear();
                Paragraph p = new Paragraph(new Run()
                {
                    Text = "Searching...",
                    FontSize = 22,
                    FontWeight = FontWeights.Bold,
                });
                p.TextAlignment = TextAlignment.Center;
                KanjiInformationDocument.Blocks.Add(p);


                //Create a list and filter non-kanji from SelectedText
                string input = Helper.FilterNonKanji(SelectedText);
                List<KanjiAPIEntry> entries = new List<KanjiAPIEntry>();

                foreach(char c in input)
                {
                    //Get entry and wait a bit so as to not overwhelm the kanjiapi.dev servers.
                    entries.Add(await KanjiAPI.GetKanjiInfoAsync(c));
                    await Task.Run(() => Thread.Sleep(50));
                }

                KanjiInformationDocument.Blocks.Clear();
                foreach(KanjiAPIEntry entry in entries)
                {
                    ApiToDocumentHelper.WriteKanjiToDocument(entry, KanjiInformationDocument, false);
                }
            }
        }

        private void OnTextAnalysisOutputChanged(TextAnalysisOutputChangedEventArgs args)
        {
            InputText = args.NewText;
        }
    }
}
