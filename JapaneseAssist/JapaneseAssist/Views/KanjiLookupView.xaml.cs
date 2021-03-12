using System;
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
    /// Interaction logic for KanjiLookupView.xaml
    /// </summary>
    public partial class KanjiLookupView : UserControl
    {
        public KanjiLookupView()
        {
            InitializeComponent();
            DataContext = new KanjiLookupViewModel();
            KanjiInformationTextBox.Document = (DataContext as KanjiLookupViewModel).KanjiInformationDocument;

            Paragraph p = new Paragraph() { TextAlignment = TextAlignment.Center };
            p.Inlines.Add(new Run()
            {
                Text = "Select a kanji to get its information.",
                FontSize = 14,
                FontWeight = FontWeights.Bold
            });
            KanjiInformationTextBox.Document.Blocks.Add(p);
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as KanjiLookupViewModel).SelectedText = (sender as TextBox).SelectedText;
        }
    }
}
