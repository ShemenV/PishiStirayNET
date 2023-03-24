using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Data;
using PishiStirayNET.Data.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStirayNET.Services
{
    public class ManufacturersService
    {
        private readonly TradeContext _tradeContext;

        public ManufacturersService(TradeContext tradeContext)
        {
            _tradeContext = tradeContext;
        }

        public async Task<List<Manufacturer>> GetAllManufacturersAsync()
        {
            return await _tradeContext.Manufacturers.ToListAsync();
        }
    }
}
