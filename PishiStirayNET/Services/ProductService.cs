
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

            List<Models.Product> products= new List<Models.Product>();

            foreach (var product in productDBs)
            {
                products.Add(new Models.Product
                {
                    Article = product.ProductArticleNumber,
                    CurrentDiscount = product.CurrentDiscount,
                    Description = product.ProductDescription,
                    Image = product.ProductPhoto
                });
            }
        }
    }
}
