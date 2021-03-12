﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using JapaneseAssist.ViewModels;

namespace JapaneseAssist.Views
{
    /// <summary>
    /// Interaction logic for WordsAnalysisView.xaml
    /// </summary>
    public partial class WordsAnalysisView : UserControl
    {
        public WordsAnalysisView()
        {
            InitializeComponent();
            DataContext = new WordsAnalysisViewModel();
            WordInformationTextBox.Document = (DataContext as WordsAnalysisViewModel).WordInformationDocument;

            Paragraph p = new Paragraph() { TextAlignment = TextAlignment.Center };
            p.Inlines.Add(new Run()
            {
                Text = "Select a word to get a jisho entry.",
                FontSize = 14,
                FontWeight = FontWeights.Bold
            });
            WordInformationTextBox.Document.Blocks.Add(p);
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as WordsAnalysisViewModel).SelectedText = (sender as TextBox).SelectedText;
        }
    }
}
