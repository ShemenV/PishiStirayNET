using CommunityToolkit.Mvvm.ComponentModel;

using PishiStirayNET.Services;
using PishiStirayNET.Views.Pages;
using System.ComponentModel;
using System.Security.Policy;
using System.Windows.Controls;

namespace PishiStirayNET.VeiwModels
{
    public partial class MainWindowViewModel : ObservableObject
    {

        private PageService _pageService;



        #region Свойства


        [ObservableProperty]
        private Page pageSource;

        #endregion

        #region Команды

        #endregion

        public MainWindowViewModel(PageService pageService)
        {
           _pageService = pageService;

            _pageService.OnPageChanged += (page) => pageSource = page;
            _pageService.ChangePage(new ProductsPage());
        }

    }
}
