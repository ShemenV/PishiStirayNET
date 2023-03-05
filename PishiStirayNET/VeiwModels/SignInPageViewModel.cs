
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Services;
using PishiStirayNET.Views.Pages;
using System.Diagnostics;

namespace PishiStirayNET.VeiwModels
{
    partial class SignInPageViewModel : ObservableObject
    {

        private readonly UserService _userService;
        private readonly PageService _pageService;

        #region Свойства
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SignInCommand))]
        public string login;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SignInCommand))]
        public string password;
        #endregion

        public SignInPageViewModel(UserService userService, PageService pageService)
        {
            _userService = userService;
            _pageService = pageService;
        }





        [RelayCommand(CanExecute = nameof(CanSignIn))]
        private void SignIn()
        {

            if (_userService.Authorization(login, password) == true)
            {
                Debug.WriteLine("Произошел вход в аккаунт");
                _pageService.ChangePage(new ProductsPage());
            }
            else
            {
                Debug.WriteLine("Неверные входные данные");
            }

        }

        private bool CanSignIn()
        {
            if (string.IsNullOrWhiteSpace(login) == true || string.IsNullOrEmpty(password) == true)
            {
                return false;
            }
            return true;
        }


        [RelayCommand]
        private void GoToProductsPage() => _pageService.ChangePage(new ProductsPage());


    }
}
