using CommunityToolkit.Mvvm.ComponentModel;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    }
}
