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
    /// Interaction logic for WordsDictionaryView.xaml
    /// </summary>
    public partial class DictionariesView : UserControl
    {
        public DictionariesView()
        {
            InitializeComponent();
            DataContext = new DictionariesViewModel();

            KanjiInformation.Document = (DataContext as DictionariesViewModel).KanjiInformationDocument;
            WordInformation.Document = (DataContext as DictionariesViewModel).WordInformationDocument;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //React to pressing enter as pressing the search button
            if (e.Key == Key.Enter)
            {
                if ((sender as TextBox) == KanjiInputTextBox)
                    (DataContext as DictionariesViewModel).SearchKanjiButton.Execute();
                if ((sender as TextBox) == WordInputTextBox)
                    (DataContext as DictionariesViewModel).SearchWordButton.Execute();
                e.Handled = true;
            }
        }
    }
}
