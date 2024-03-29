﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Models;
using PishiStirayNET.Services;
using PishiStirayNET.Views.Pages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PishiStirayNET.VeiwModels
{

    public partial class CartPageViewModel : ObservableObject
    {
        private readonly ProductService _productService;
        private readonly OrderService _orderService;
        private readonly DocumentService _documentSevice;
        private readonly SaveFileDialogService _saveFileDialogService;
        private readonly PageService _pageService;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalPrice))]
        private ObservableCollection<CartItem>? cartProductsList;

        [ObservableProperty]

        [NotifyCanExecuteChangedFor(nameof(CreateOrderCommand))]
        private CartItem? selectedCartItem;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalCount))]
        [NotifyPropertyChangedFor(nameof(TotalPrice))]
        [NotifyPropertyChangedFor(nameof(TotalDiscount))]
        [NotifyPropertyChangedFor(nameof(ResultCost))]
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


        public float? TotalPrice
        {
            get => CartProductsList.Sum(item => item.Cost);
        }

        public float? TotalDiscount
        {
            get => CartProductsList.Sum(item => item.Discount);
        }

        public float? ResultCost
        {
            get => TotalPrice - TotalDiscount;
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




        public CartPageViewModel(ProductService productService, OrderService orderService, DocumentService documentSevice, SaveFileDialogService saveFileDialogService, PageService pageService)
        {
            _productService = productService;
            _orderService = orderService;

            cartProductsList = Global.CartProductList;

            Task.Run(async () =>
            {
                IssuePoints = await _orderService.GetIssuePointsAsync();
            });
            _documentSevice = documentSevice;
            _saveFileDialogService = saveFileDialogService;
            _pageService = pageService; 
        }


        [RelayCommand]
        private void IncreaseSelectedCartItemCount()
        {
            if (SelectedCartItem != null && SelectedCartItem.Count > 0 && Count < SelectedCartItem.Product.ProductQuantityInStock)
            {
                SelectedCartItem.Count++;
                Count = SelectedCartItem.Count;
                Debug.WriteLine(SelectedCartItem.Cost);

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

        [RelayCommand]
        private void GoBackPage()
        {
            _pageService.ChangePage(new ProductsPage());
        }

        private bool CanChangeCount()
        {
            return SelectedCartItem != null && SelectedCartItem.Count > 0 && Count < SelectedCartItem.Product.ProductQuantityInStock;
        }


        [RelayCommand(CanExecute = nameof(CanCreateOrder))]
        private async void CreateOrder()
        {
            Order order = await _orderService.CreateOrder(CartProductsList.ToList(), SelectedIssuepoint.IdPunkta);

            

            string selectedFolder = "";
            selectedFolder = _saveFileDialogService.PDFSaveFileDialog();
            if (selectedFolder != "no folder")
            {
                _documentSevice.CreateDocument(order, selectedFolder);
            }
           
            CartProductsList.Clear();
            _pageService.ChangePage(new ProductsPage());
        }

        private bool CanCreateOrder()
        {
            if (SelectedIssuepoint != null && CartProductsList.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
