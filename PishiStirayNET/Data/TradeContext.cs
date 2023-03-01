using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Models.DbEntities;

namespace PishiStirayNET.Data
{
    internal class TradeContext : DbContext
    {
        public DbSet<UserDB> User { get; set; }

        public TradeContext(DbContextOptions<TradeContext> options) :
            base(options)
        { }
    }
}
