using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Data;
using PishiStirayNET.Data.DbEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PishiStirayNET.Services
{
    public class DeliveriesService
    {
        private readonly TradeContext _tradeContext;

        public DeliveriesService(TradeContext tradeContext)
        {
            _tradeContext = tradeContext;
        }
        public async Task<List<Delivery>> GetAllDeliveriesAsync()
        {
            return await _tradeContext.Deliveries.ToListAsync();
        }
    }
}
