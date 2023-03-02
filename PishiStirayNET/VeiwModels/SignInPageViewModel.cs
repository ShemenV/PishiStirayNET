
using CommunityToolkit.Mvvm.ComponentModel;
using PishiStirayNET.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PishiStirayNET.VeiwModels
{
    partial class SignInPageViewModel :ObservableObject
    {

        private readonly UserService _userService;
        private readonly PageService _pageService;

        #region Свойства
        [ObservableProperty]
        public string login;

        [ObservableProperty]
        public string password;
        #endregion

        public SignInPageViewModel(UserService userService, PageService pageService)
        {
            _userService = userService;
            _pageService = pageService; 
        }


        public AsyncCommand SignInCommand => new(async ()=>
        {           
            await Task.Run(async () =>
            {
                //if (_userService.Authorization(Login, Password) == true)
                //{
                //    Debug.WriteLine("Произошел вход в аккаунт");
                //}
                //else
                //{
                //    Debug.WriteLine("Неверные входные данные");
                //}
                _pageService.ChangePage(new ProductsPage());
            });       
            },
            bool () =>
            {
                if(string.IsNullOrWhiteSpace(Login) == true || string.IsNullOrEmpty(Password) == true)
                {
                    return false;
                }
                return true;
            }
            );           
    }
}
