using System;
using System.Collections.Generic;
using System.Text;

using JapaneseAssistLib.Models;

namespace JapaneseAssistLib.Events
{
    public delegate void TextAnalysisOutputChanged(TextAnalysisOutputChangedEventArgs eventArgs);

    public class TextAnalysisOutputChangedEventArgs: EventArgs
    {
        public string NewText { get; private set; }
        public IEnumerable<FoundKanji> FoundKanji { get; private set; }
        public TextAnalysisOutputChangedEventArgs(string newText, IEnumerable<FoundKanji> foundKanji)
        {
            NewText = newText;
            FoundKanji = foundKanji;
        }
    }
}
