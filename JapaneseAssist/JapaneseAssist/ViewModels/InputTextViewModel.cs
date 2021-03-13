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

        private int _AppendedLineBreaks;
        public int AppendedLineBreaks
        {
            get
            {
                return _AppendedLineBreaks;
            }
            set
            {
                _AppendedLineBreaks = value;
                OnPropertyChanged();
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
            AppendedLineBreaks = 1;
        }

        private void OnClipboardContentChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (MonitorClipboard && e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                string addedLines = "";
                for(int i = 0; i < AppendedLineBreaks; i++)
                {
                    addedLines += "\n";
                }
                InputText += addedLines + (string)e.Content;
            }
        }
    }
}
