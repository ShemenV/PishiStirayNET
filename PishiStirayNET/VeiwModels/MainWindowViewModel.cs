using CommunityToolkit.Mvvm.ComponentModel;

using PishiStirayNET.Services;
using System.ComponentModel;
using System.Security.Policy;

namespace PishiStirayNET.VeiwModels
{
    public partial class MainWindowViewModel : ObservableObject
    {

        private UserService _userService;


        #region Свойства

        [ObservableProperty]
        public string? title = "This is amazing";

        

        #endregion

        #region Команды

        #endregion

        public MainWindowViewModel()
        {


        }

    }
}
