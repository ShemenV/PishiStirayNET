using PishiStirayNET.Data;
using PishiStirayNET.Models.DbEntities;
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
            UserDB user = _trade.User.Where(user => user.UserLogin == userLogin && user.UserPassword == userPassword).SingleOrDefault();

            if (user != null)
            {
                Debug.WriteLine("not null");
                return true;
            }
            Debug.WriteLine("null");
            return false;
        }
    }
}
