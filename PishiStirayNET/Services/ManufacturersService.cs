using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Data;
using PishiStirayNET.Data.DbEntities;
using System.Collections.Generic;
using System.Linq;
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

        public async void ChangeManufacturer(Manufacturer manufacturer)
        {
            Manufacturer newManufacturer = await _tradeContext.Manufacturers.Where(m => m.IdManafacturer == manufacturer.IdManafacturer).FirstOrDefaultAsync();

            if (newManufacturer != null)
            {
                newManufacturer.Name = manufacturer.Name;
                await _tradeContext.SaveChangesAsync();
            }
        }


        public async void AddManufacturer(Manufacturer manufacturer)
        {
            manufacturer.IdManafacturer = await _tradeContext.Manufacturers.MaxAsync(m => m.IdManafacturer) + 1;
            _tradeContext.Manufacturers.Add(manufacturer);
            await _tradeContext.SaveChangesAsync();
        }


    }
}
