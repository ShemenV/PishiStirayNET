using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PishiStirayNET.VeiwModels
{
    public partial class DeliveriesPageViewModel : ObservableValidator
    {
        private readonly DeliveriesService _deliveriesService;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private string? _name;

        [ObservableProperty]
        private List<Delivery> _deliveriesList;

        [ObservableProperty]
        private Delivery _selectedDelivery;

        [ObservableProperty]
        private bool _isChanged = false;

        public DeliveriesPageViewModel(DeliveriesService deliveriesService)
        {
            _deliveriesService = deliveriesService;
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            DeliveriesList = await _deliveriesService.GetAllDeliveriesAsync();
            Name = string.Empty;
        }

        [RelayCommand]
        private void ChangeDelivery()
        {
            IsChanged = true;
            Name = SelectedDelivery.Name;
        }

        [RelayCommand]
        private async void SendDelivery()
        {
            Delivery delivery = new ();
            delivery.Name = Name;

            if (IsChanged == true)
            {
                delivery.IdProvider = SelectedDelivery.IdProvider;
                _deliveriesService.ChangeDelivery(delivery);
                IsChanged = false;
                await Task.Delay(200);
                LoadDataAsync();
                return;
            }
            else
            {

                _deliveriesService.AddDelivery(delivery);
                await Task.Delay(90);
                LoadDataAsync();
            }

        }

        [RelayCommand]
        private void CancelChange()
        {
            IsChanged = false;
            Name = string.Empty;
            SelectedDelivery = null;
        }
    }
}
