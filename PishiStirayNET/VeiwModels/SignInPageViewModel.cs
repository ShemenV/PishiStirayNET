using DevExpress.Mvvm;
using PishiStirayNET.Services;
using PishiStirayNET.Views.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStirayNET.VeiwModels
{
    class SignInPageViewModel : ViewModelBase
    {

        private readonly UserService _userService;
        private readonly PageService _pageService;

        #region Свойства
        public string Login { get; set; }
        public string Password { get; set; }
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
