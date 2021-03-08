using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Collections.Generic;

using JapaneseAssistLib;
using JapaneseAssistLib.Events;
using JapaneseAssistLib.API;
using JapaneseAssistLib.Models;

namespace JapaneseAssist.ViewModels
{
    class WordsAnalysisViewModel : ViewModelBase
    {
        //This will be implemented later once I have an SQLite database with a Japanese word dictionary.
        /// <summary>
        /// Information about a specific word in a format readable by the user
        /// </summary>
        public readonly FlowDocument WordInformationDocument;
        private string _InputText;
        /// <summary>
        /// Japanese text inputted by the user
        /// </summary>
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
                if(_SelectedText != value)
                {
                    _SelectedText = value;
                    _ = WaitAndGetEntry();
                }
            }
        }

        public WordsAnalysisViewModel()
        {
            WordInformationDocument = new FlowDocument();
            TextAnalyzer.OutputChanged += OnTextAnalyzerOutputChanged;
            GetEntry(new List<JishoEntry>());
        }

        private async Task WaitAndGetEntry()
        {
            string helper = SelectedText;
            await Task.Run(() => Thread.Sleep(750));
            if (helper == SelectedText && helper != "")
                GetEntry(await JishoAPI.GetJishoEntry(SelectedText));
        }

        public void GetEntry(List<JishoEntry> entries)
        {
            WordInformationDocument.Blocks.Clear();

            //Display amount of found entries
            Paragraph p = new Paragraph() { TextAlignment = TextAlignment.Center };

            if(entries.Count == 0)
            {
                p.Inlines.Add(new Run()
                {
                    Text = "Select a word to get a jisho entry.",
                    FontSize = 14,
                    FontWeight = FontWeights.Bold
                });
            }
            else
            {
                p.Inlines.Add(new Run()
                {
                    Text = "Found " + entries.Count + " entries.",
                    FontSize = 14,
                    FontWeight = FontWeights.Bold
                });
            }

            WordInformationDocument.Blocks.Add(p);

            foreach (JishoEntry e in entries)
            {

                p = new Paragraph();

                AddMainReadWrite(e, p);

                if (e.IsCommon || e.Jlpt.Count > 0 || e.Tags.Count > 0)
                    p.Inlines.Add(new Run("\n"));

                AddTags(e, p);
                WordInformationDocument.Blocks.Add(p);

                p = new Paragraph();

                AddSenses(e, p);
                WordInformationDocument.Blocks.Add(p);

                p = new Paragraph();

                AddOtherForms(e, p);
                if(p.Inlines.Count > 0)
                    WordInformationDocument.Blocks.Add(p);

                WordInformationDocument.Blocks.Add(new Paragraph());
            }
        }

        private void AddMainReadWrite(JishoEntry entry, Paragraph paragraphToWriteTo)
        {
            paragraphToWriteTo.Inlines.Add(new Run()
            {
                FontSize = 14,
                Text = entry.Japanese[0].Reading + "\n"
            });
            paragraphToWriteTo.Inlines.Add(new Run()
            {
                FontSize = 22,
                Text = entry.Japanese[0].Word
            });
        }

        private void AddTags(JishoEntry entry, Paragraph paragraphToWriteTo)
        {
            BrushConverter bc = new BrushConverter();

            //Common word tag
            if (entry.IsCommon)
            {
                Border b = new Border();
                b.Width = 100;
                b.CornerRadius = new CornerRadius(3);
                b.Background = (Brush)bc.ConvertFromString("#43AC6A");
                b.Child = new TextBlock()
                {
                    Text = "common word",
                    Foreground = Brushes.White,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Center
                };
                paragraphToWriteTo.Inlines.Add(b);
                paragraphToWriteTo.Inlines.Add(new Run(" "));
            }

            //JLPT tags
            foreach (string s in entry.Jlpt)
            {
                Border b = new Border();
                b.Width = 100;
                b.CornerRadius = new CornerRadius(3);
                b.Background = (Brush)bc.ConvertFromString("#909dc0");
                b.Child = new TextBlock()
                {
                    Text = s,
                    Foreground = Brushes.White,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Center
                };
                paragraphToWriteTo.Inlines.Add(b);
                paragraphToWriteTo.Inlines.Add(new Run(" "));
            }

            //Wanikani tags
            foreach (string s in entry.Tags)
            {
                if (s.Contains("wanikani"))
                {
                    Border b = new Border();
                    b.Width = 100;
                    b.CornerRadius = new CornerRadius(3);
                    b.Background = (Brush)bc.ConvertFromString("#909dc0");
                    b.Child = new TextBlock()
                    {
                        Text = s,
                        Foreground = Brushes.White,
                        FontWeight = FontWeights.Bold,
                        TextAlignment = TextAlignment.Center
                    };
                    paragraphToWriteTo.Inlines.Add(b);
                    paragraphToWriteTo.Inlines.Add(new Run(" "));
                }
            }
        }

        private void AddSenses(JishoEntry entry, Paragraph paragraphToWriteTo)
        {
            for (int i = 0; i < entry.Senses.Count; i++)
            {
                //Display part of speech if it's the first definition or the current senses parts of speech isn't the same as the previous one
                if (i == 0 || !entry.Senses[i].PartsOfSpeech.SequenceEqual(entry.Senses[i - 1].PartsOfSpeech))
                {
                    paragraphToWriteTo.Inlines.Add(new Run()
                    {
                        FontSize = 16,
                        Foreground = Brushes.Gray,
                        Text = String.Join(", ", entry.Senses[i].PartsOfSpeech) + "\n"
                    });
                }

                Run englishDef = new Run()
                {
                    FontSize = 20,
                    Text = (i + 1).ToString() + ". " + String.Join(", ", entry.Senses[i].EnglishDefinitions)
                };

                //Add an additional line to the english definition if it isn't the last one, so the definitions aren't on one line
                if (i + 1 != entry.Senses.Count)
                    englishDef.Text += "\n";

                paragraphToWriteTo.Inlines.Add(englishDef);
            }
        }

        private void AddOtherForms(JishoEntry entry, Paragraph paragraphToWriteTo)
        {
            if (entry.Japanese.Count > 1)
            {
                paragraphToWriteTo.Inlines.Add(new Run()
                {
                    FontSize = 14,
                    Text = "Other forms:\n"
                });
            }

            for (int i = 1; i < entry.Japanese.Count; i++)
            {
                paragraphToWriteTo.Inlines.Add(new Run()
                {
                    FontSize = 14,
                    Text = entry.Japanese[i].Word + " [" + entry.Japanese[i].Reading + "]、"
                });
            }
        }

        void OnTextAnalyzerOutputChanged(TextAnalysisOutputChangedEventArgs eventArgs)
        {
            InputText = String.Join("。\n", eventArgs.NewText.Split('。'));
        }
    }
}
