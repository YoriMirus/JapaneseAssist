using System;
using System.Collections.Generic;
using System.Text;

namespace JapaneseAssist.Events
{
    internal delegate void InputTextChanged(InputTextChangedEventArgs eventArgs);

    internal class InputTextChangedEventArgs: EventArgs
    {
        public string NewText { get; private set; }
        public InputTextChangedEventArgs(string newText)
        {
            NewText = newText;
        }
    }
}
