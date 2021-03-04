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
                }
            }
        }

        public InputTextViewModel()
        {
            InputText = "Write your text here.";
        }
    }
}
