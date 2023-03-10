﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Models;
using PishiStirayNET.Services;
using PishiStirayNET.Views.Pages;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace PishiStirayNET.VeiwModels
{
    public partial class ProductsPageViewModel : ObservableObject
    {
        private readonly ProductService _productService;
        private readonly PageService _pageService;

        #region Свойства

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

        [ObservableProperty]
        private int currentProductsCount;

        [ObservableProperty]
        private int totalProductsCount;

        [ObservableProperty]
        private List<Product> productsList;

        [ObservableProperty]
        private Product selectedProduct;

        [ObservableProperty]
        private Visibility cartVisibility = Visibility.Visible;

        #region Changed методы
        partial void OnSearchQueryChanged(string? value)
        {
            UpdateProductsList();
        }

        partial void OnSelectedOrderChanged(string? value)
        {
            UpdateProductsList();
        }

        partial void OnSelectedFilterChanged(string? value)
        {
            UpdateProductsList();
        }
        #endregion

        #endregion



        public ProductsPageViewModel(ProductService productService, PageService pageService)
        {
            _productService = productService;
            _pageService = pageService;

            Debug.WriteLine("ProductsPageViewMode created");

            SelectedFilter = filtersList[0];
        }

        public async void UpdateProductsList()
        {
            List<Product> products = await _productService.GetProductsAsync();
            TotalProductsCount = products.Count;


            if (SearchQuery != null)
            {
                products = products.Where(p => p.Title.ToLower().Trim().Contains(SearchQuery.ToLower().Trim())).ToList();
            }

            switch (SelectedFilter)
            {
                case "Все диапазоны":

                    products = products.ToList();

                    break;

                case "0 - 9,99%":
                    products = products.Where(p => p.CurrentDiscount >= 0 && p.CurrentDiscount <= 9.99 || p.CurrentDiscount == null).ToList();
                    break;

                case "10 - 14,99%":
                    products = products.Where(p => p.CurrentDiscount >= 10 && p.CurrentDiscount <= 14.99).ToList();
                    break;

                case "15% и более":
                    products = products.Where(p => p.CurrentDiscount >= 15).ToList();
                    break;
            }

            switch (SelectedOrder)
            {
                case "По возрастанию":

                    products = products.OrderBy(p =>
                    {
                        if (p.NewPrice != null)
                        {
                            return p.NewPrice;
                        }
                        return p.Price;
                    }).ToList();

                    break;

                case "По убыванию":
                    products = products.OrderByDescending(p =>
                    {
                        if (p.NewPrice != null)
                        {
                            return p.NewPrice;
                        }
                        return p.Price;
                    }).ToList();
                    break;
            }

            ProductsList = products;

            CurrentProductsCount = products.Count;
        }


        [RelayCommand]
        private void AddProductToCart()
        {
            if (SelectedProduct != null)
            {
                CartItem? cartItem = Cart.CartProductList.SingleOrDefault(p => p.Product.Article == SelectedProduct.Article);
                if (cartItem == null)
                {
                    Cart.CartProductList.Add(new CartItem
                    {
                        Product = SelectedProduct,
                       Count = 1
                       
                    });
                }
                else
                {
                    cartItem.Count++;
                    Debug.WriteLine(Cart.CartProductList.IndexOf(cartItem));
                }

            }
        }

      

        [RelayCommand]
        private void GoToCart()
        {
            _pageService.ChangePage(new CartPage());
        }
    }
}
