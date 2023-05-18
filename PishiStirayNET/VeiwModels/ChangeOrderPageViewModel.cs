using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Models;
using PishiStirayNET.Services;
using PishiStirayNET.Views.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PishiStirayNET.VeiwModels
{
    public partial class ChangeOrderPageViewModel : ObservableObject
    {
        private readonly PageService _pageService;
        private readonly OrderService _orderService;


        [ObservableProperty]
        private Order changedOrder;

        [ObservableProperty]
        private List<OrderStatus> statuses;

        [ObservableProperty]
        private OrderStatus selectedStatus;

        [ObservableProperty]
        private DateTime selectedEndDate;

        public ChangeOrderPageViewModel(OrderService orderService, PageService pageService)
        {
            _orderService = orderService;

            LoadData();
            _pageService = pageService;
        }

        private async void LoadData()
        {
            await Task.Delay(100);
            Statuses = await _orderService.GetOrderStatuses();
            ChangedOrder = Global.Order;
            SelectedStatus = ChangedOrder.OrderStatusNavigation;
            SelectedEndDate = ChangedOrder.OrderDeliveryDateEnd;
        }


        [RelayCommand]
        private async void SaveOrder()
        {
            ChangedOrder.OrderStatus = SelectedStatus.IdOrderStatus;
            ChangedOrder.OrderDeliveryDateEnd = SelectedEndDate;
            _orderService.ChangeOrder(ChangedOrder);
            _pageService.ChangePage(new OrdersPage());
        }

        [RelayCommand]
        private void GoBackPage()
        {
            _pageService.ChangePage(new OrdersPage());
        }
    }
}
