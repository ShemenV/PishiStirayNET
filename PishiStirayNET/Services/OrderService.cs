using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Data;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PishiStirayNET.Services
{
    public class OrderService
    {
        private readonly TradeContext _tradeContext;

        public OrderService(TradeContext tradeContext)
        {
            _tradeContext = tradeContext;
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
            int receiptСode = _tradeContext.Order1s.Max(o => o.CodePoluch) + 1;

            await _tradeContext.Order1s.AddAsync(new Order1
            {
                OrderId = orderNumber,
                OrderStatus = 2,
                OrderDeliveryDate = DateTime.Now,
                OrderDeliveryDateEnd = DateTime.Now.AddYears(6),
                OrderPickupPoint = issuepointID,
                Fio = CurrentUser.User != null ? $"{CurrentUser.User.UserSurname} {CurrentUser.User.UserName} {CurrentUser.User.UserPatronymic}" : null,
                CodePoluch = receiptСode
            });


            List<Orderproduct> orderproductList = new List<Orderproduct>();
            foreach (CartItem cartItem in cartItems)
            {
                orderproductList.Add(new Orderproduct
                {
                    OrderId = orderNumber,
                    ProductArticleNumber = cartItem.Product.ProductArticleNumber,
                    Count = cartItem.Count
                });
            }

            foreach (CartItem cartItem in cartItems)
            {
                ProductDB? product = await _tradeContext.Products.Where(p => p.ProductArticleNumber == cartItem.Product.ProductArticleNumber).SingleOrDefaultAsync();

                if (product != null)
                {
                    product.ProductQuantityInStock -= cartItem.Count;
                }
            }

            await _tradeContext.Orderproducts.AddRangeAsync(orderproductList);

            await _tradeContext.SaveChangesAsync();
        }


        public async Task<List<Order>> GetAllOrdersAsync()
        {
            List<Order1> order1s = await _tradeContext.Order1s.ToListAsync();
            List<Order> orders = new List<Order>();
            foreach (Order1 order1 in order1s)
            {

                orders.Add(new Order
                {
                    OrderId = order1.OrderId,
                    CodePoluch = order1.CodePoluch,
                    Fio = order1.Fio,
                    OrderDeliveryDate = order1.OrderDeliveryDate,
                    OrderDeliveryDateEnd = order1.OrderDeliveryDateEnd,
                    OrderPickupPoint = order1.OrderPickupPoint,
                    OrderPickupPointNavigation = order1.OrderPickupPointNavigation,
                    OrderStatus = order1.OrderStatus,
                    OrderStatusNavigation = order1.OrderStatusNavigation,
                    FullPrice = (float)order1.Orderproducts.ToList().Sum(op => op.Count * op.ProductArticleNumberNavigation.ProductCost),
                    Discount = (float)order1.Orderproducts.ToList().Sum(op => op.Count * (Convert.ToSingle(op.ProductArticleNumberNavigation.ProductCost) / 100 * (float)op.ProductArticleNumberNavigation.CurrentDiscount)) / ((float)order1.Orderproducts.ToList().Sum(op => op.Count * op.ProductArticleNumberNavigation.ProductCost) / 100),
                    ProductQuatities = GetProductsQuatities(order1.Orderproducts),
                    Products = await GetProducts(order1.Orderproducts),

                });

            }
            return orders;
        }

        private List<int> GetProductsQuatities(ICollection<Orderproduct> orderproducts)
        {
            List<int> quantities = new List<int>();

            foreach (var product in orderproducts.ToList())
            {
                quantities.Add(product.ProductArticleNumberNavigation.ProductQuantityInStock);
            }

            return quantities;
        }

        private async Task<List<Product>> GetProducts(ICollection<Orderproduct> orderproducts)
        {
            List<Product> products = new List<Product>();

            foreach(var product in orderproducts.ToList())
            {
                products.Add(new Product
                {
                    ProductArticleNumber = product.ProductArticleNumberNavigation.ProductArticleNumber,
                    CurrentDiscount = product.ProductArticleNumberNavigation.CurrentDiscount,
                    ProductDescription = product.ProductArticleNumberNavigation.ProductDescription,
                    Image = (product.ProductArticleNumberNavigation.ProductPhoto == null || string.IsNullOrWhiteSpace(product.ProductArticleNumberNavigation.ProductPhoto) == true) ? "picture.png" : product.ProductArticleNumberNavigation.ProductPhoto,
                    ProductCost = (product.ProductArticleNumberNavigation.ProductCost),
                    ProductManufacturerNavigation = product.ProductArticleNumberNavigation.ProductManufacturerNavigation,
                    ProductName = product.ProductArticleNumberNavigation.ProductName,
                    ProductQuantityInStock = product.ProductArticleNumberNavigation.ProductQuantityInStock,
                    ProductCategoryNavigation = product.ProductArticleNumberNavigation.ProductCategoryNavigation,
                    Delivery = product.ProductArticleNumberNavigation.Delivery,
                    UnitOfMeasurementNavigation = product.ProductArticleNumberNavigation.UnitOfMeasurementNavigation,
                    ProductDiscountAmount = product.ProductArticleNumberNavigation.ProductDiscountAmount,

                });
            }

            return products;
        }
    }
}
