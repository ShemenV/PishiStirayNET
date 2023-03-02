using DevExpress.Mvvm;
using PishiStirayNET.Models.DbEntities;
using PishiStirayNET.Services;
using PishiStirayNET.Views.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PishiStirayNET.VeiwModels
{
    internal class MainWindowViewModel: ViewModelBase
    {

        private PageService _pageService;

        public Page PageSource { get; set; }

        #region Свойства

        

        #endregion
         
        #region Команды

        #endregion

        public MainWindowViewModel(PageService pageService)
        {
           _pageService= pageService;
            _pageService.Page = new SignInPage();
            PageSource = _pageService.Page;
        }
        
    }
}
