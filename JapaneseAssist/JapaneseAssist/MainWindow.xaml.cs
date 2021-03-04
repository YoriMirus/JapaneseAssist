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
using JapaneseAssist.Events;

namespace JapaneseAssist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly InputTextView inputTextView;
        private readonly KanjiAnalysisView kanjiAnalysisView;
        
        public MainWindow()
        {
            InitializeComponent();
            inputTextView = new InputTextView();
            kanjiAnalysisView = new KanjiAnalysisView();

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
                        break;
                    case "KanjiAnalysis":
                        WindowContent.Content = kanjiAnalysisView;
                        break;
                }
            }
        }
    }
}
