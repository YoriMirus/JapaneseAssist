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
        }
    }
}
