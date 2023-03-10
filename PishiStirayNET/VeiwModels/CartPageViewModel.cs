using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Models;
using PishiStirayNET.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace PishiStirayNET.VeiwModels
{
    public partial class CartPageViewModel : ObservableObject
    {
        private readonly ProductService _productService;
        private readonly OrderService _orderService;

        [ObservableProperty]
        private ObservableCollection<CartItem>? cartProductsList;

        [ObservableProperty]
        private CartItem? selectedCartItem;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalCount))]
        private int? count;

        [ObservableProperty]
        private List<Issuepoint> issuePoints;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateOrderCommand))]
        private Issuepoint? selectedIssuepoint;

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




        public CartPageViewModel(ProductService productService, OrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
            
            cartProductsList = Cart.CartProductList;

            Task.Run(async () =>
            {
                IssuePoints = await _orderService.GetIssuePointsAsync();
            });
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


        private bool CanChangeCount()
        {
            if (SelectedCartItem != null && SelectedCartItem.Count > 0 && SelectedCartItem.Product.)
            {
                return true;
            }
            return false;
        }


        [RelayCommand(CanExecute = nameof(CanCreateOrder))]
        private async void CreateOrder()
        {
            _orderService.CreateOrder(CartProductsList.ToList(), SelectedIssuepoint.IdPunkta);
        }

        private bool CanCreateOrder()
        {
            if(SelectedIssuepoint!= null)
            {
                return true;
            }
            return false;
        }
    }
}
