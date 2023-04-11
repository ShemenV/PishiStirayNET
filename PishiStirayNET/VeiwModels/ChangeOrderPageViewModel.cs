using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Models;
using PishiStirayNET.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PishiStirayNET.VeiwModels
{
    public partial class ChangeOrderPageViewModel : ObservableObject
    {
        private readonly OrderService _orderService;


        [ObservableProperty]
        private Order changedOrder;

        [ObservableProperty]
        private List<OrderStatus> statuses;

        [ObservableProperty]
        private OrderStatus selectedStatus;

        [ObservableProperty]
        private DateTime selectedEndDate;

        public ChangeOrderPageViewModel(OrderService orderService)
        {
            _orderService = orderService;

            LoadData();
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
            Debug.WriteLine("gtgtgtgtgt");
        }
    }
}
