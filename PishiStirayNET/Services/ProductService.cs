using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Data;
using System.Collections.Generic;
using System.Diagnostics;
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


        public async Task<List<Models.Product>> GetProductsAsync(string? searchQuery = null, string? orderValue = null, string? filterValue = null)
        {
            List<Models.Product> products = new();
            List<ProductDB> productDBs = await _context.Products.ToListAsync();
            await _context.Manufacturers.ToListAsync();


            await Task.Run(() =>
            {
                Debug.WriteLine(productDBs.Count);
                foreach (ProductDB product in productDBs)
                {
                    products.Add(new Models.Product
                    {
                        Article = product.ProductArticleNumber,
                        CurrentDiscount = product.CurrentDiscount,
                        Description = product.ProductDescription,
                        Image = product.ProductPhoto,
                        Price = ((float)product.ProductCost),
                        Manufacturer = product.ProductManufacturerNavigation.Name,
                        Title = product.ProductName
                    });
                }

            });



            Debug.Write(products.Count);
            return products;
        }



        //public async Task<List<Product>> GetProductFromCartAsync()
        //{
        //    List<Product> cartProducts = new();
        //    List<Product> products = await GetProductsAsync();

        //    await Task.Run(() =>
        //    {
        //        foreach (Product product in products)
        //        {
        //            CartItem? cartItem = Cart.CartProductList.Where(i => i.Article == product.Article).FirstOrDefault();

        //            if(cartItem != null)
        //            {

        //            }
        //        }
        //    });

        //    return cartProducts;
        //}
    }
}
