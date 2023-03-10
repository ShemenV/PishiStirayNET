using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Models;
using PishiStirayNET.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Printing;

namespace PishiStirayNET.VeiwModels
{
    public partial class CartPageViewModel : ObservableObject
    {
        private readonly ProductService _productService;

        [ObservableProperty]
        private ObservableCollection<CartItem>? cartProductsList;

        [ObservableProperty]
        private CartItem? selectedCartItem;

        [ObservableProperty]
        private int? count;

        partial void OnCountChanged(int? value)
        {
            Debug.WriteLine("Count changed");
            if (Count == 0)
            {
                CartProductsList.Remove(SelectedCartItem);
            }
        }

        partial void OnSelectedCartItemChanged(CartItem? value)
        {
            if(SelectedCartItem != null)
            {
                Count = SelectedCartItem.Count;
            }
            
        }

        public CartPageViewModel(ProductService productService)
        {
            _productService = productService;

            cartProductsList = Cart.CartProductList;

        }

        private async void Update()
        {
            //productList = await _productService.GetProductFromCartAsync();
        }

        [RelayCommand]
        private void IncreaseSelectedCartItemCount()
        {
            if (SelectedCartItem != null && SelectedCartItem.Count > 0)
            {
                SelectedCartItem.Count++;
                Count = SelectedCartItem.Count;
                Debug.WriteLine(SelectedCartItem.Count);
            }

        }

        [RelayCommand]
        private void DecreaseSelectedCartItemCount()
        {
            if (SelectedCartItem != null && SelectedCartItem.Count > 0)
            {
                --SelectedCartItem.Count;
                Count = SelectedCartItem.Count;
            }
        }
    }
}
