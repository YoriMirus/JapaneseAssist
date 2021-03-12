﻿using System;
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
            if (helper == SelectedText && !string.IsNullOrEmpty(SelectedText))
            {
                KanjiInformationDocument.Blocks.Clear();
                string input = Helper.FilterNonKanji(SelectedText);
                List<KanjiAPIEntry> entries = new List<KanjiAPIEntry>();

                foreach(char c in input)
                {
                    //Get entry and wait a bit so as to not overwhelm the kanjiapi.dev servers.
                    entries.Add(await KanjiAPI.GetKanjiInfoAsync(c));
                    await Task.Run(() => Thread.Sleep(50));
                }

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
