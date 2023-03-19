using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Data;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            await _context.ProductCategories.ToListAsync();
            await _context.Deliveries.ToListAsync();
            await _context.Units.ToListAsync();


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
                        Image = (product.ProductPhoto == null || string.IsNullOrWhiteSpace(product.ProductPhoto) == true) ? "picture.png" : product.ProductPhoto,
                        Price = ((float)product.ProductCost),
                        Manufacturer = product.ProductManufacturerNavigation,
                        Title = product.ProductName,
                        MaxQuantity = product.ProductQuantityInStock,
                        Category = product.ProductCategoryNavigation,
                        Delivery = product.DeliveryNavigation,
                        Unit = product.UnitOfMeasurementNavigation,
                        MaxDiscount = product.ProductDiscountAmount
                    });
                }

            });



            Debug.Write(products.Count);
            return products;
        }



        public async Task<List<Product>> GetProductFromCartAsync()
        {
            List<Product> cartProducts = new();
            List<Product> products = await GetProductsAsync();

            await Task.Run(() =>
            {
                foreach (Product product in products)
                {
                    CartItem? cartItem = Cart.CartProductList.Where(i => i.Product.Article == product.Article).FirstOrDefault();
                    if (cartItem != null)
                    {

                    }
                }
            });

            return cartProducts;
        }


        public async Task<List<ProductCategory>> GetProductCategoriesAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<List<Manufacturer>> GetManufacturersAsync()
        {
            return await _context.Manufacturers.ToListAsync();
        }


        public async Task<List<Delivery>> GetDeliveriesAsync()
        {
            return await _context.Deliveries.ToListAsync();
        }

        public async Task<List<Unit>> GetUnitAsync()
        {
            return await _context.Units.ToListAsync();
        }

        public async void AddNewProduct(ProductDB productDB)
        {
            await _context.Products.AddAsync(productDB);
            await _context.SaveChangesAsync();
        }


        public async Task<string> GenerateArticle()
        {
            string article = "";
            List<ProductDB> articles = await _context.Products.ToListAsync();

            await Task.Run(() =>
            {
                string[] symbolsArray = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Z" };

                bool isArticle = false;

                while (isArticle == false)
                {
                    Random rnd = new();
                    for (int i = 0; i < 5; i = i + 1)
                    {
                        article = article + symbolsArray[rnd.Next(0, symbolsArray.Length)];
                    }

                    if (articles.All(a => a.ProductArticleNumber != article))
                    {
                        isArticle = true;
                    }
                }

            });

            return article;
        }

        public async void ChangeProduct(ProductDB productDB)
        {
            ProductDB? product = await _context.Products.Where(p => p.ProductArticleNumber == productDB.ProductArticleNumber).SingleOrDefaultAsync();

            if (product != null)
            {
                product.ProductManufacturer = productDB.ProductManufacturer;
                product.ProductPhoto = productDB.ProductPhoto;
                product.CurrentDiscount = productDB.CurrentDiscount;
                product.Delivery = productDB.Delivery;
                product.ProductCategory = productDB.ProductCategory;
                product.ProductDescription = productDB.ProductDescription;
                product.ProductName = productDB.ProductName;
                product.UnitOfMeasurement = productDB.UnitOfMeasurement;
                product.ProductCost = productDB.ProductCost;
                product.ProductQuantityInStock = productDB.ProductQuantityInStock;

                _context.SaveChanges();
            }
        }




        public async void DeleteProduct(Product productDB)
        {
            ProductDB? product = await _context.Products.Where(p => p.ProductArticleNumber == productDB.Article).SingleOrDefaultAsync();

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
