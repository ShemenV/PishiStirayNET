using PishiStirayNET.Views.Windows.Interfaces;
using System.Windows;

namespace PishiStirayNET.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window, IWindow
    {
        public AddProductWindow()
        {
            InitializeComponent();
        }

        public bool? Open()
        {
            return this.ShowDialog();
        }


    }
}
