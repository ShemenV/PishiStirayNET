using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Services;
using PishiStirayNET.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace PishiStirayNET.VeiwModels
{
    public partial class AddProductPageViewModel : ObservableValidator
    {
        private readonly ProductService _productService;
        private readonly PageService _pageService;
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
        private float? price;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [Range(0, 100, ErrorMessage = "Не более 6 символов и только числа")]
        private float? currentDiscount;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [Range(0, 100, ErrorMessage = "Не более 2 символов и только числа")]
        private int? maxDiscount;


        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [Range(0, 1000000, ErrorMessage = "Не более 6 символов и только числа")]
        private int? maxCount;


        [ObservableProperty]
        private string selectedPath = "picture.png";

        [ObservableProperty]
        private BitmapImage imagePath = new(new Uri($"/Resources/picture.png", UriKind.Relative));



        public AddProductPageViewModel(ProductService productService, SaveFileDialogService saveFileDialogService, PageService pageService)
        {
            _productService = productService;
            _saveFileDialogService = saveFileDialogService;
            _pageService = pageService;



            LoadProductData();


        }

        protected virtual async void LoadProductData()
        {
            try
            {
                ProductCategories = await _productService.GetProductCategoriesAsync();
                Manufacturers = await _productService.GetManufacturersAsync();
                Deliveries = await _productService.GetDeliveriesAsync();
                Units = await _productService.GetUnitAsync();

                if (Global.Product != null)
                {

                    SelectedCategory = ProductCategories[ProductCategories.IndexOf(Global.Product.ProductCategoryNavigation)];
                    SelectedManufacturer = Global.Product.ProductManufacturerNavigation;
                    SelectedDelivery = Global.Product.DeliveryNavigation;
                    SelectedUnit = Global.Product.UnitOfMeasurementNavigation;
                    Title = Global.Product.ProductName;
                    Description = Global.Product.ProductDescription;
                    Price = (float?)Global.Product.ProductCost;
                    CurrentDiscount = Global.Product.CurrentDiscount;
                    MaxCount = Global.Product.ProductQuantityInStock;
                    SelectedPath = Global.Product.Image;

                }
            }
            catch (Exception ex)
            {

            }
        }

        [RelayCommand]
        private void AddPhoto()
        {
            SelectedPath = _saveFileDialogService.SaveFileDialog();
            ImagePath = new(new Uri(Path.GetFullPath($"Resources/{SelectedPath}"), UriKind.Absolute));
            Debug.WriteLine(SelectedPath);
        }

        [RelayCommand]
        private async void AddNewProduct()
        {
            ValidateAllProperties();

            if (HasErrors == false)
            {

                _productService.AddNewProduct(new ProductDB
                {
                    ProductArticleNumber = await _productService.GenerateArticle(),
                    ProductName = Title,
                    ProductDescription = Description,
                    ProductCategory = SelectedCategory.IdCategory,
                    ProductPhoto = SelectedPath,
                    ProductManufacturer = SelectedManufacturer.IdManafacturer,
                    ProductCost = (decimal)Price,
                    CurrentDiscount = (sbyte?)CurrentDiscount,
                    ProductQuantityInStock = (int)MaxCount,
                    ProductDiscountAmount = MaxDiscount,
                    UnitOfMeasurement = SelectedUnit.IdUnit,
                    Delivery = SelectedDelivery.IdProvider,
                });

                _pageService.ChangePage(new ProductsPage());
            }


        }
    }
}
