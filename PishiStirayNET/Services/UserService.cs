using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Data;
using PishiStirayNET.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStirayNET.Services
{
    internal class UserService
    {
        private readonly TradeContext _trade;

        public UserService(TradeContext trade)
        {
            _trade = trade;
        }

        public UserDB Authorization(string userLogin, string userPassword)
        {
            UserDB user = _trade.User.Where(user => user.UserLogin == userLogin).SingleOrDefault();
            return user;
        }
    }
}
