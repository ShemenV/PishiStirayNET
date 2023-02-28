using PishiStirayNET.Models.DbEntities;
using PishiStirayNET.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStirayNET.VeiwModels
{
    internal class MainWindowViewModel
    {

        private UserService _userService;


        #region Свойства

        

        #endregion
         
        #region Команды

        #endregion

        public MainWindowViewModel(UserService userService)
        {
            _userService = userService;
           
        }
        
    }
}
