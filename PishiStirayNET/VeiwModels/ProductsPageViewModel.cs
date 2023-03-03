using CommunityToolkit.Mvvm.ComponentModel;
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
        private List<ProductDB> productsList;

        public ProductsPageViewModel(ProductService productService)
        {
            _productService = productService;
            
            

           productsList = _productService.GetProducts();
        }
    }
}
