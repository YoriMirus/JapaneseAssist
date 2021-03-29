using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using JapaneseAssist.Views;

namespace JapaneseAssist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly InputTextView inputTextView;
        private readonly KanjiAnalysisView kanjiAnalysisView;
        private readonly KanjiLookupView kanjiLookupView;
        private readonly WordsLookupView wordsAnalysisView;
        private readonly DictionariesView dictionariesView;
        
        public MainWindow()
        {
            InitializeComponent();
            inputTextView = new InputTextView();
            kanjiAnalysisView = new KanjiAnalysisView();
            kanjiLookupView = new KanjiLookupView();
            wordsAnalysisView = new WordsLookupView();
            dictionariesView = new DictionariesView();

            WindowContent.Content = inputTextView;
        }

        private void SetContent(object sender, RoutedEventArgs e)
        {
            if (sender is Button source)
            {
                switch ((string)source.Tag)
                {
                    case "InputText":
                        WindowContent.Content = inputTextView;
                        CurrentSectionName.Text = "Input text";
                        break;
                    case "KanjiAnalysis":
                        WindowContent.Content = kanjiAnalysisView;
                        CurrentSectionName.Text = "Kanji analysis";
                        break;
                    case "KanjiLookup":
                        WindowContent.Content = kanjiLookupView;
                        CurrentSectionName.Text = "Kanji lookup";
                        break;
                    case "WordsLookup":
                        WindowContent.Content = wordsAnalysisView;
                        CurrentSectionName.Text = "Words lookup";
                        break;
                    case "Dictionaries":
                        WindowContent.Content = dictionariesView;
                        CurrentSectionName.Text = (string)source.Tag;
                        break;
                }
            }
        }

        private void OnTopWindowGridMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void MinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButtonClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TopPannelButtonSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is Button b)
            {
                b.Width = b.ActualHeight;
            }
        }
    }
}
