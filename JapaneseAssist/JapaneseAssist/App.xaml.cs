using System.Windows;

using JapaneseAssistLib;

namespace JapaneseAssist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            TextAnalyzer.Initialize();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
    }
}
