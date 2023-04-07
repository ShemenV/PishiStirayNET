using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Data;
using PishiStirayNET.Data.DbEntities;
using System.Collections.Generic;
using System.Linq;
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

        public async void ChangeDelivery(Delivery delivery)
        {
            Delivery newDelivery = await _tradeContext.Deliveries.Where(d => d.IdProvider.Equals(delivery.IdProvider)).FirstOrDefaultAsync();
            
            if(newDelivery != null)
            {
                newDelivery.Name= delivery.Name;
                await _tradeContext.SaveChangesAsync();
            }
        }

        public async void AddDelivery(Delivery delivery)
        {
            delivery.IdProvider = await _tradeContext.Deliveries.MaxAsync(d => d.IdProvider) +1;
            await _tradeContext.Deliveries.AddAsync(delivery);
            await _tradeContext.SaveChangesAsync();
        }
    }
}
