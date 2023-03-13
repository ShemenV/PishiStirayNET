using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Data;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PishiStirayNET.Services
{
    public class OrderService
    {
        private readonly TradeContext _tradeContext;

        public OrderService(TradeContext tradeContext) 
        { 
            _tradeContext= tradeContext;
        }


        public async Task<List<Issuepoint>> GetIssuePointsAsync()
        {
           List<Issuepoint> issuePoints = new List<Issuepoint>();

            await Task.Run(async () =>
            {
                issuePoints = await _tradeContext.Issuepoints.AsNoTracking().ToListAsync();
            });

            return issuePoints;
        }

        public async void CreateOrder(List<CartItem> cartItems, int issuepointID)
        {
            int orderNumber = _tradeContext.Order1s.Max(o => o.OrderId) + 1;
            int receiptСode = _tradeContext.Order1s.Max(o => o.CodePoluch) +1;

            await _tradeContext.Order1s.AddAsync(new Order1
            {
                OrderId = orderNumber,
                OrderStatus = 2,
                OrderDeliveryDate = DateTime.Now,
                OrderDeliveryDateEnd = DateTime.Now.AddYears(6),
                OrderPickupPoint = issuepointID,
                Fio = CurrentUser.User != null ? $"{CurrentUser.User.UserSurname} {CurrentUser.User.UserName} {CurrentUser.User.UserPatronymic}" : null,
                CodePoluch = receiptСode
            }) ;


            List<Orderproduct> orderproductList = new List<Orderproduct>();
            foreach(CartItem cartItem in cartItems)
            {
                orderproductList.Add(new Orderproduct
                {
                    OrderId = orderNumber,
                    ProductArticleNumber = cartItem.Product.Article,
                    Count = cartItem.Count
                });
            }

            await _tradeContext.Orderproducts.AddRangeAsync(orderproductList);

           await _tradeContext.SaveChangesAsync();

        }
    }
}
