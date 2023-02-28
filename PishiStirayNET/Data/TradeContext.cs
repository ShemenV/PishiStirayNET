using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStirayNET.Data
{
    internal class TradeContext: DbContext
    {
        public DbSet<UserDB> User { get; set; }

        public TradeContext(DbContextOptions<TradeContext> options) :
            base(options){ }
    }
}
