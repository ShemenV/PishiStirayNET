using CommunityToolkit.Mvvm.ComponentModel;
using PishiStirayNET.Models;
using PishiStirayNET.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PishiStirayNET.VeiwModels
{
    public partial class ProductsPageViewModel : ObservableObject
    {
        private readonly ProductService _productService;

        [ObservableProperty]
        private string? searchQuery;

        [ObservableProperty]
        private List<string> orderList = new() { "По убыванию", "По возрастанию" };

        [ObservableProperty]
        private List<string> filtersList = new() { "Все диапазоны", "0 - 9,99%", "10 - 14,99%", "15% и более" };

        [ObservableProperty]
        private string? selectedFilter;

        [ObservableProperty]
        private string? selectedOrder;


        partial void OnSelectedOrderChanged(string? value)
        {
            UpdateProductsList();
        }

        partial void OnSelectedFilterChanged(string? value)
        {
            UpdateProductsList();
        }


        [ObservableProperty]
        private List<Product> productsList;

        public ProductsPageViewModel(ProductService productService)
        {
            _productService = productService;

            Debug.WriteLine("ProductsPageViewMode created");
            SelectedFilter = filtersList[0];
        }

        public async void UpdateProductsList()
        {
            List<Product> products = await _productService.GetProductsAsync();

            switch (SelectedOrder)
            {
                case "По возрастанию":

                    products = products.OrderBy(p => p.Price).ToList();

                    break;

                case "По убыванию":
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;
            }

            ProductsList = products;
        }
    }
}
