using System;
using System.Collections.Generic;
using System.Text;

using JapaneseAssist.Events;

namespace JapaneseAssist.ViewModels
{
    class InputTextViewModel: ViewModelBase
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
                if (value != _InputText)
                {
                    _InputText = value;
                    OnPropertyChanged();
                    InputTextChanged?.Invoke(new InputTextChangedEventArgs(value));
                }
            }
        }

        public event InputTextChanged InputTextChanged;

        public InputTextViewModel(InputTextChanged onInputTextChanged)
        {
            InputText = "Write your text here.";
            InputTextChanged += onInputTextChanged;
        }
    }
}
