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
    /// Interaction logic for KanjiAnalysisView.xaml
    /// </summary>
    public partial class KanjiAnalysisView : UserControl
    {
        public KanjiAnalysisView()
        {
            InitializeComponent();
            DataContext = new KanjiAnalysisViewModel();
        }

        private void ListViewSizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gv = (sender as ListView).View as GridView;

            foreach(GridViewColumn column in gv.Columns)
            {
                column.Width = e.NewSize.Width / gv.Columns.Count;
            }
        }
    }
}
