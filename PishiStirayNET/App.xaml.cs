using System.Windows;

namespace PishiStirayNET
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DI.Init();
            base.OnStartup(e);
        }
    }
}
