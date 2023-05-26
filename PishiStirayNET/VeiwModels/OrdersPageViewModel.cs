using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Models;
using PishiStirayNET.Services;
using PishiStirayNET.Views.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PishiStirayNET.VeiwModels
{
    public partial class OrdersPageViewModel : ObservableValidator
    {
        private readonly OrderService _orderService;
        private readonly PageService _pageService;

        public OrdersPageViewModel(OrderService orderService, PageService pageService)
        {
            _orderService = orderService;
            _pageService = pageService;

            SelectedFilter = FiltersList[0];
            SelectedStatus = StatusesList[0];
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

        [ObservableProperty]
        private List<string> statusesList = new() { "Все статусы", "Завершен", "Новый" };

        [ObservableProperty]
        private string? selectedStatus;





        partial void OnSelectedSortChanged(string? value)
        {
            UpdateOrdersList();
        }



        partial void OnSelectedFilterChanged(string? value)
        {
            UpdateOrdersList();
        }

        partial void OnSelectedStatusChanged(string? value)
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

            switch (SelectedStatus)
            {
                case "Все статусы":
                    orders = orders.ToList();

                    break;

                case "Завершен":
                    orders = orders.Where(p => p.OrderStatus == 1).ToList();
                    break;
                case "Новый":
                    orders = orders.Where(p => p.OrderStatus == 2).ToList();
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

        [RelayCommand]
        private void GoBackPage()
        {       
                _pageService.ChangePage(new ProductsPage());
        }

        [RelayCommand]
        private void GoToChangePage()
        {
            if (SelectedOrder != null)
            {
                Global.Order = SelectedOrder;
                _pageService.ChangePage(new ChangeOrderPage());
            }
        }
    }
}
