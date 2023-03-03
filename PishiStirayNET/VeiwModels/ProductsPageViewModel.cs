using CommunityToolkit.Mvvm.ComponentModel;
using PishiStirayNET.Models;
using PishiStirayNET.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStirayNET.VeiwModels
{
    public partial class ProductsPageViewModel: ObservableObject
    {
        private readonly ProductService _productService;

       

        [ObservableProperty]
        private List<Product> productsList;

        public ProductsPageViewModel(ProductService productService)
        {
            _productService = productService;



            productsList = _productService.GetProducts();
        }
    }
}
