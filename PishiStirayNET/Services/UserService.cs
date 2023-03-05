
using PishiStirayNET.Data;
using PishiStirayNET.Infrastructure;
using System.Diagnostics;
using System.Linq;

namespace PishiStirayNET.Services
{
    internal class UserService
    {
        private readonly TradeContext _trade;

        public UserService(TradeContext trade)
        {
            _trade = trade;
        }

        public bool Authorization(string userLogin, string userPassword)
        {
            UserDB user = _trade.Users.Where(user => user.UserLogin == userLogin && user.UserPassword == userPassword).SingleOrDefault();

            if (user != null)
            {

                CurrentUser.User = new Models.User
                {
                    UserID = user.UserId,
                    UserName = user.UserName,
                    UserLogin = user.UserLogin,
                    UserPassword = user.UserPassword,
                    UserPatronymic = user.UserPatronymic,
                    UserRole = user.UserRoleNavigation.RoleName,
                    UserSurname = user.UserSurname
                };

                return true;
            }
            Debug.WriteLine("null");
            return false;
        }
    }
}
