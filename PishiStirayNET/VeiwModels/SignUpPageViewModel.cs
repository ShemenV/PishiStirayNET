using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Services;
using PishiStirayNET.Views.Pages;
using System.ComponentModel.DataAnnotations;

namespace PishiStirayNET.VeiwModels
{
    public partial class SignUpPageViewModel : ObservableValidator
    {
        private readonly UserService _userService;
        private readonly PageService _pageService;


        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private string login;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private string password;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private string name;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private string surname;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private string patronymic;


        public SignUpPageViewModel(UserService userService, PageService pageService)
        {
            _userService = userService;
            _pageService = pageService;
        }


        [RelayCommand]
        private void SignUpUser()
        {
            ValidateAllProperties();

            if (HasErrors == false)
            {
                _userService.SignUp(new Data.DbEntities.UserDB
                {
                    UserName = Name,
                    UserPassword = Password,
                    UserSurname = Surname,
                    UserPatronymic = Patronymic,
                    UserLogin = Login
                });
                _pageService.ChangePage(new SignInPage());
            }

        }
    }
}
