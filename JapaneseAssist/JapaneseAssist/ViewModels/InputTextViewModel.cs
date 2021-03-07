using System;
using System.Collections.Generic;
using System.Text;

using JapaneseAssistLib;

namespace JapaneseAssist.ViewModels
{
    class InputTextViewModel: ViewModelBase
    {
        private string _InputText;
        /// <summary>
        /// Japanese text written by the user
        /// </summary>
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
                    TextAnalyzer.InputText = _InputText;
                }
            }
        }

        public InputTextViewModel()
        {
            //Currently don't really need anything here.
        }
    }
}
