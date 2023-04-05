using CommunityToolkit.Mvvm.ComponentModel;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Models;
using PishiStirayNET.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStirayNET.VeiwModels
{
    public partial class OrdersPageViewModel: ObservableValidator
    {
        private readonly OrderService _orderService;

        public OrdersPageViewModel(OrderService orderService)
        {
            _orderService= orderService;

            UpdateOrdersList();
        }


        [ObservableProperty]
        private List<Order> ordersList;

        [ObservableProperty]
        private Order selectedOrder;



        private async void UpdateOrdersList()
        {
            OrdersList = await _orderService.GetAllOrdersAsync();
        }
      
    }
}
