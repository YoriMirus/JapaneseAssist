using System;
using System.Windows;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Text;
using JapaneseAssistLib.Models;
using System.Windows.Media;
using System.Linq;
using System.Windows.Controls;

namespace JapaneseAssist.Helpers
{
    internal static class ApiToDocumentHelper
    {
        /// <summary>
        /// Writes Jisho entries to a FlowDocument
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="WordInformationDocument"></param>
        /// <param name="clearDocument"></param>
        public static void WriteJishoToDocument(List<JishoEntry> entries, FlowDocument WordInformationDocument, bool clearDocument)
        {
            if (clearDocument)
                WordInformationDocument.Blocks.Clear();

            //Display amount of found entries
            Paragraph p = new Paragraph() { TextAlignment = TextAlignment.Center };

            p.Inlines.Add(new Run()
            {
                Text = "Found " + entries.Count + " entries.",
                FontSize = 14,
                FontWeight = FontWeights.Bold
            });
        
            WordInformationDocument.Blocks.Add(p);

            foreach (JishoEntry e in entries)
            {
                p = new Paragraph();

                AddMainReadWrite(e, p);

                //If any tags were added, add "\n"
                if (e.IsCommon || e.Jlpt.Count > 0 || e.Tags.Count > 0)
                    p.Inlines.Add(new Run("\n"));

                //Add tags
                AddTags(e, p);
                WordInformationDocument.Blocks.Add(p);

                //Add senses
                p = new Paragraph();
                AddSenses(e, p);
                WordInformationDocument.Blocks.Add(p);

                //Add other forms
                p = new Paragraph();
                AddOtherForms(e, p);

                //p should be added only if there is something in it, otherwise makes a blank space that I don't want
                if (p.Inlines.Count > 0)
                    WordInformationDocument.Blocks.Add(p);

                WordInformationDocument.Blocks.Add(new Paragraph());
            }
        }
        private static void AddMainReadWrite(JishoEntry entry, Paragraph paragraphToWriteTo)
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
        private static void AddTags(JishoEntry entry, Paragraph paragraphToWriteTo)
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
        private static void AddSenses(JishoEntry entry, Paragraph paragraphToWriteTo)
        {
            for (int i = 0; i < entry.Senses.Count; i++)
            {
                Senses sense = entry.Senses[i];
                //Display part of speech if it's the first definition or the current senses parts of speech isn't the same as the previous one
                if (i == 0 || !sense.PartsOfSpeech.SequenceEqual(entry.Senses[i - 1].PartsOfSpeech))
                {
                    paragraphToWriteTo.Inlines.Add(new Run()
                    {
                        FontSize = 16,
                        Foreground = Brushes.Gray,
                        Text = sense.GetPartsOfSpeech() + "\n"
                    });
                }

                Run englishDef = new Run()
                {
                    FontSize = 20,
                    Text = (i + 1).ToString() + ". " + sense.GetEnglishDefinitions()
                };

                //Add an additional line to the english definition if it isn't the last one, so the definitions aren't on one line
                if (i + 1 != entry.Senses.Count)
                    englishDef.Text += "\n";

                paragraphToWriteTo.Inlines.Add(englishDef);
            }
        }
        private static void AddOtherForms(JishoEntry entry, Paragraph paragraphToWriteTo)
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

        public static void WriteKanjiToDocument(KanjiAPIEntry entry, FlowDocument KanjiInformationDocument, bool clearDocument)
        {
            //Remove previous entry from the document
            if(clearDocument)
                KanjiInformationDocument.Blocks.Clear();

            //Display character
            Paragraph p = new Paragraph();
            p.TextAlignment = TextAlignment.Center;

            p.Inlines.Add(new Run()
            {
                Text = entry.Kanji,
                FontSize = 30,
                FontWeight = FontWeights.Bold
            });
            KanjiInformationDocument.Blocks.Add(p);


            //Display kanji meanings
            p = new Paragraph();
            p.TextAlignment = TextAlignment.Left;

            p.Inlines.Add(new Run()
            {
                Text = "Meaning: " + entry.GetMeanings() + "\n",
                FontSize = 16,
            });


            //Display kanji kunyomi readings
            p.Inlines.Add(new Run()
            {
                Text = "Kunyomi: " + entry.GetKunyomi() + "\n",
                FontSize = 16,
            });
            KanjiInformationDocument.Blocks.Add(p);


            //Display kanji onyomi readings
            p.Inlines.Add(new Run()
            {
                Text = "Onyomi: " + entry.GetOnyomi() + "\n",
                FontSize = 16,
            });
            KanjiInformationDocument.Blocks.Add(p);


            //Display JLPT
            p.Inlines.Add(new Run()
            {
                Text = "JLPT: N" + entry.Jlpt,
                FontSize = 16,
            });
            KanjiInformationDocument.Blocks.Add(p);
        }
    }
}
