using System;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Text;

using JapaneseAssistLib;
using JapaneseAssistLib.API;
using JapaneseAssistLib.Models;
using JapaneseAssistLib.Helpers;

using JapaneseAssist.Models;
using JapaneseAssist.Helpers;

namespace JapaneseAssist.ViewModels
{
    class DictionariesViewModel : ViewModelBase
    {
        private string _WordInput;
        /// <summary>
        /// A word that the user inputted
        /// </summary>
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
                SearchWordButton?.FireCanExecuteChanged();
            }
        }

        private string _KanjiInput;
        /// <summary>
        /// Kanji that the user inputted
        /// </summary>
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
                SearchKanjiButton?.FireCanExecuteChanged();
            }
        }

        private ButtonCommand _SearchWordButton;
        /// <summary>
        /// Search button for the kanji dictionary
        /// </summary>
        public ButtonCommand SearchWordButton
        {
            get
            {
                return _SearchWordButton;
            }
            set
            {
                _SearchWordButton = value;
                OnPropertyChanged();
            }
        }

        private ButtonCommand _SearchKanjiButton;
        /// <summary>
        /// Search button for the Japanese word dictionary
        /// </summary>
        public ButtonCommand SearchKanjiButton
        {
            get
            {
                return _SearchKanjiButton;
            }
            set
            {
                _SearchKanjiButton = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A kanji search result in a format readable by the user.
        /// </summary>
        public readonly FlowDocument KanjiInformationDocument;
        /// <summary>
        /// A word search result in a format readable by the user.
        /// </summary>
        public readonly FlowDocument WordInformationDocument;

        private async void SearchKanji()
        {
            KanjiInformationDocument.Blocks.Clear();


            //Notify the user that a search has started
            Paragraph p = new Paragraph(new Run()
            {
                Text = "Searching...",
                FontSize = 22,
                FontWeight = FontWeights.Bold
            });
            KanjiInformationDocument.Blocks.Add(p);


            string input = Helper.FilterNonKanji(KanjiInput);
            KanjiInformationDocument.Blocks.Clear();
            foreach (char c in input)
            {
                KanjiAPIEntry entry = await KanjiAPI.GetKanjiInfoAsync(c);
                ApiToDocumentHelper.WriteKanjiToDocument(entry, KanjiInformationDocument, false);

                //Wait for a bit in case there is a lot of kanji so that the servers don't get overwhelmed
                await Task.Run(() => Thread.Sleep(100));
            }
        }

        private async void SearchWord()
        {
            WordInformationDocument.Blocks.Clear();

            Paragraph p = new Paragraph(new Run()
            {
                FontSize = 22,
                Text = "Searching...",
                FontWeight = FontWeights.Bold,
            });
            p.TextAlignment = TextAlignment.Center;
            WordInformationDocument.Blocks.Add(p);

            List<JishoEntry> entries = await JishoAPI.GetJishoEntry(WordInput);
            ApiToDocumentHelper.WriteJishoToDocument(entries, WordInformationDocument, true);
        }

        public DictionariesViewModel()
        {
            KanjiInput = "";
            WordInput = "";

            KanjiInformationDocument = new FlowDocument();
            WordInformationDocument = new FlowDocument();

            SearchKanjiButton = new ButtonCommand(SearchKanji, () => KanjiInput.Length > 0);
            SearchWordButton = new ButtonCommand(SearchWord, () => WordInput.Length > 0);
        }
    }
}
