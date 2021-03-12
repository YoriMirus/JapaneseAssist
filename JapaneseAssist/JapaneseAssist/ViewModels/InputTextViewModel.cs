using System;
using System.Collections.Generic;
using System.Text;
using WK.Libraries.SharpClipboardNS;

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

        private bool _MonitorClipboard;
        public bool MonitorClipboard
        {
            get
            {
                return _MonitorClipboard;
            }
            set
            {
                _MonitorClipboard = value;
                OnPropertyChanged();
            }
        }

        public InputTextViewModel()
        {
            TextAnalyzer.ClipboardMonitorer.ClipboardChanged += OnClipboardContentChanged;
        }

        private void OnClipboardContentChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (MonitorClipboard && e.ContentType == SharpClipboard.ContentTypes.Text)
                InputText += "\n" + (string)e.Content;
        }
    }
}
