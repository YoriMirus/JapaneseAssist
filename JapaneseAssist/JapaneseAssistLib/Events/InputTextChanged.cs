using System;
using System.Collections.Generic;
using System.Text;

using JapaneseAssistLib.Models;

namespace JapaneseAssistLib.Events
{
    public delegate void InputTextChanged(InputTextChangedEventArgs eventArgs);

    public class InputTextChangedEventArgs: EventArgs
    {
        public string NewText { get; private set; }
        public List<FoundKanji> FoundKanji { get; private set; }
        public InputTextChangedEventArgs(string newText, List<FoundKanji> foundKanji)
        {
            NewText = newText;
            FoundKanji = foundKanji;
        }
    }
}
