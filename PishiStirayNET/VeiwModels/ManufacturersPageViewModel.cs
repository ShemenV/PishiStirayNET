using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStirayNET.VeiwModels
{
    public partial class ManufacturersPageViewModel: ObservableValidator
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
        }

        [RelayCommand]
        private void ChangeManufacturer()
        {
            IsChanged= true;
            Name = SelectedManufacturer.Name;   
        }

        [RelayCommand]
        private void CancelChange()
        {
            IsChanged= false;
            Name = string.Empty;
            SelectedManufacturer = null;
        }

    }
}
