using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Models;
using PishiStirayNET.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

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
        [NotifyPropertyChangedFor(nameof(TotalCount))]
        private int? count;

        public int? TotalCount
        {
            get => CartProductsList.Sum(item => item.Count);
        }

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
            if (SelectedCartItem != null)
            {
                Count = SelectedCartItem.Count;

            }

        }




        public CartPageViewModel(ProductService productService)
        {
            _productService = productService;

            cartProductsList = Cart.CartProductList;


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
