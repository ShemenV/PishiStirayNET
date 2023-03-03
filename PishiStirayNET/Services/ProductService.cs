
using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStirayNET.Services
{
    public class ProductService
    {
        private readonly TradeContext _context;

        public ProductService(TradeContext context)
        {
            _context = context;
        }


        public List<Models.Product> GetProducts()
        {
            List<ProductDB> productDBs = _context.Products.ToList();
            foreach(var productDB in productDBs)
            {
                Debug.WriteLine(productDB.ProductManufacturerNavigation.Name);
            }

            List<Models.Product> products = new List<Models.Product>();

            foreach (var product in productDBs)
            {
                products.Add(new Models.Product
                {
                    Article = product.ProductArticleNumber,
                    CurrentDiscount = product.CurrentDiscount,
                    Description = product.ProductDescription,
                    Image = product.ProductPhoto,
                    Price = product.ProductCost,
                    Manufacturer = product.ProductManufacturerNavigation.Name,
                    Title = product.ProductName
                });
            }
            return products;
        }
    }
}
