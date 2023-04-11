using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Services;
using PishiStirayNET.Views.Pages;

namespace PishiStirayNET.VeiwModels
{
    public partial class AuthorizedUserUserControlViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? fullname;

        [ObservableProperty]
        private string? role;

        [ObservableProperty]
        private bool isAuthorized;


        private readonly PageService _pageService;

        public AuthorizedUserUserControlViewModel(PageService pageService)
        {

            _pageService = pageService;
            if (Global.User != null)
            {
                Fullname = Global.User.UserSurname + " " + Global.User.UserName + " " + Global.User.UserPatronymic;
                Role = Global.User.UserRoleNavigation.RoleName;
                IsAuthorized = true;
            }
            else
            {
                IsAuthorized = false;
            }

        }


        [RelayCommand]
        private void LogOut()
        {
            Global.User = null;
            IsAuthorized = false;
            _pageService.ChangePage(new SignInPage());
        }

    }
}
