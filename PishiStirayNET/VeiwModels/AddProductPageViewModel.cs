using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace PishiStirayNET.VeiwModels
{
    public partial class AddProductPageViewModel : ObservableValidator
    {
        private readonly ProductService _productService;
        private readonly SaveFileDialogService _saveFileDialogService;

        [ObservableProperty]
        private List<ProductCategory> productCategories;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private ProductCategory selectedCategory;

        [ObservableProperty]
        private List<Manufacturer> manufacturers;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private Manufacturer selectedManufacturer;

        [ObservableProperty]
        private List<Delivery> deliveries;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private Delivery selectedDelivery;

        [ObservableProperty]
        private List<Unit> units;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private Unit selectedUnit;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [MinLength(2, ErrorMessage = "Название должно быть длинее 2 символов")]
        [MaxLength(100, ErrorMessage = "Название не должно быть длинее 100 символов")]
        private string? title;


        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [MinLength(2, ErrorMessage = "Описание должно быть длинее 2 символов")]
        [MaxLength(100, ErrorMessage = "Описание не должно быть длинее 100 символов")]
        private string? description;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [Range(0, 1000000, ErrorMessage = "Не более 6 символов и только числа")]
        private int? price;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [Range(0, 100, ErrorMessage = "Не более 6 символов и только числа")]
        private int? currentDiscount;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [Range(0, 100, ErrorMessage = "Не более 2 символов и только числа")]
        private int? maxDiscount;


        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [Range(0, 1000000, ErrorMessage = "Не более 6 символов и только числа")]
        private int? currentCount;


        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [Range(0, 1000000, ErrorMessage = "Не более 6 символов и только числа")]
        private int? maxCount;


        [ObservableProperty]
        private Uri selectedPath = new Uri("/Resources/picture.png", UriKind.Relative);



        public AddProductPageViewModel(ProductService productService, SaveFileDialogService saveFileDialogService)
        {
            _productService = productService;

            LoadProductData();
            _saveFileDialogService = saveFileDialogService;
        }

        private async void LoadProductData()
        {
            ProductCategories = await _productService.GetProductCategoriesAsync();
            Manufacturers = await _productService.GetManufacturersAsync();
            Deliveries = await _productService.GetDeliveriesAsync();
            Units = await _productService.GetUnitAsync();
        }

        [RelayCommand]
        private void AddPhoto()
        {


            SelectedPath = new Uri($"/Resources/{_saveFileDialogService.SaveFileDialog()}", UriKind.Relative);
            Debug.WriteLine(SelectedPath);

        }

        [RelayCommand]
        private void AddNewProduct()
        {
            ValidateAllProperties();



        }
    }
}
