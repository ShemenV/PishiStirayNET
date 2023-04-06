using CommunityToolkit.Mvvm.ComponentModel;
using PishiStirayNET.Models;
using PishiStirayNET.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PishiStirayNET.VeiwModels
{
    public partial class OrdersPageViewModel : ObservableValidator
    {
        private readonly OrderService _orderService;

        public OrdersPageViewModel(OrderService orderService)
        {
            _orderService = orderService;

            SelectedFilter = filtersList[0];
        }


        [ObservableProperty]
        private List<Order> ordersList;

        [ObservableProperty]
        private Order selectedOrder;

        [ObservableProperty]
        private List<string> sortList = new() { "По убыванию", "По возрастанию" };

        [ObservableProperty]
        private List<string> filtersList = new() { "Все диапазоны", "0 - 9,99%", "10 - 14,99%", "15% и более" };

        [ObservableProperty]
        private string? selectedFilter;

        [ObservableProperty]
        private string? selectedSort;

        partial void OnSelectedSortChanged(string? value)
        {
            UpdateOrdersList();
        }

        private void UpdateProductsList()
        {
            throw new NotImplementedException();
        }

        partial void OnSelectedFilterChanged(string? value)
        {
            UpdateOrdersList();
        }


        private async void UpdateOrdersList()
        {
            List<Order> orders = await _orderService.GetAllOrdersAsync();

            switch (SelectedFilter)
            {
                case "Все диапазоны":
                    orders = orders.ToList();

                    break;
                case "0 - 9,99%":
                    orders = orders.Where(p => p.Discount >= 0 && p.Discount <= 9.99 || p.Discount == null).ToList();
                    break;
                case "10 - 14,99%":
                    orders = orders.Where(p => p.Discount >= 10 && p.Discount <= 14.99).ToList();
                    break;
                case "15% и более":
                    orders = orders.Where(p => p.Discount >= 15).ToList();
                    break;
            }

            switch (SelectedSort)
            {
                case "По возрастанию":

                    orders = orders.OrderBy(p =>
                    {

                        return p.FullPrice;
                    }).ToList();

                    break;

                case "По убыванию":
                    orders = orders.OrderByDescending(p =>
                    {

                        return p.FullPrice;
                    }).ToList();
                    break;
            }
            OrdersList = orders;
        }

    }
}
