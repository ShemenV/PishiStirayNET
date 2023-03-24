using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PishiStirayNET.VeiwModels
{
    public partial class ManufacturersPageViewModel : ObservableValidator
    {
        private readonly ManufacturersService _manufacturersService;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private string? _name;

        [ObservableProperty]
        private List<Manufacturer> _manufacturersList;

        [ObservableProperty]
        private Manufacturer _selectedManufacturer;

        [ObservableProperty]
        private bool _isChanged = false;

        public ManufacturersPageViewModel(ManufacturersService manufacturersService)
        {
            _manufacturersService = manufacturersService;
            LoadDataAsync();


        }

        private async void LoadDataAsync()
        {
            ManufacturersList = await _manufacturersService.GetAllManufacturersAsync();
            Name = string.Empty;
        }

        [RelayCommand]
        private void ChangeManufacturer()
        {
            IsChanged = true;
            Name = SelectedManufacturer.Name;
        }

        [RelayCommand]
        private void CancelChange()
        {
            IsChanged = false;
            Name = string.Empty;
            SelectedManufacturer = null;
        }


        [RelayCommand]
        private async void SendManufacturer()
        {
            Manufacturer manufacturer = new Manufacturer();
            manufacturer.Name = Name;

            if (IsChanged == true)
            {
             manufacturer.IdManafacturer = SelectedManufacturer.IdManafacturer;
                manufacturer.Name = Name;
                _manufacturersService.ChangeManufacturer(manufacturer);
                IsChanged = false;
                await Task.Delay(200);
                LoadDataAsync();
                return;
            }
            else
            {

                _manufacturersService.AddManufacturer(manufacturer);
                await Task.Delay(90);
                LoadDataAsync();
            }

        }

    }
}
