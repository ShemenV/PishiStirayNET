
using CommunityToolkit.Mvvm.ComponentModel;
using PishiStirayNET.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PishiStirayNET.VeiwModels
{
    partial class SignInPageViewModel :ObservableObject
    {

        private readonly UserService _userService;

        #region Свойства
        [ObservableProperty]
        public string login;

        [ObservableProperty]
        public string password;
        #endregion

        public SignInPageViewModel(UserService userService)
        {
            _userService = userService;
        }


    //    public AsyncCommand SignInCommand => new(async () =>
    //    {
    //        await Task.Run(async () =>
    //        {
    //            if (_userService.Authorization(Login, Password) == true)
    //            {
    //                Debug.WriteLine("Произошел вход в аккаунт");
    //            }
    //            else
    //            {
    //                Debug.WriteLine("Неверные входные данные");
    //            }
    //        });
    //    },
    //        bool () =>
    //        {
    //            if (string.IsNullOrWhiteSpace(Login) == true || string.IsNullOrEmpty(Password) == true)
    //            {
    //                return false;
    //            }
    //            return true;
    //        }
    //        );
    }
}
