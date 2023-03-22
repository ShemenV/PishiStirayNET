
using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Data;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace PishiStirayNET.Services
{
    public class UserService
    {
        private readonly TradeContext _trade;

        public UserService(TradeContext trade)
        {
            _trade = trade;
        }

        public async Task<bool> Authorization(string userLogin, string userPassword)
        {
            UserDB user = await _trade.Users.Where(user => user.UserLogin == userLogin && user.UserPassword == userPassword).SingleOrDefaultAsync();


            if (user != null)
            {

                CurrentUser.User = user;

                return true;
            }
            return false;

        }

        public async void SignUp(UserDB user)
        {
            UserDB userDB = user;
            userDB.UserId = _trade.Users.Max(u => u.UserId) + 1;
            userDB.UserRole = 2;

            await _trade.Users.AddAsync(userDB);
            await _trade.SaveChangesAsync();
        }
    }
}
