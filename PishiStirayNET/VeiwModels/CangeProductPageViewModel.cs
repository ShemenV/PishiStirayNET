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
    public partial class CangeProductPageViewModel : ObservableValidator
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



        public CangeProductPageViewModel(ProductService productService, SaveFileDialogService saveFileDialogService, PageService pageService)
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

                if (ChangedProduct.Product != null && ProductCategories != null)
                {

                    SelectedCategory = ProductCategories[0];

                    int index = 0;
                    for (int i = 0; i < ProductCategories.Count; i++)
                    {
                        if (ProductCategories[i].IdCategory == ChangedProduct.Product.Category.IdCategory)
                        {
                            index = i; break;
                        }
                    }
                    SelectedCategory = ProductCategories[index];

                    index = 0;
                    for (int i = 0; i < Manufacturers.Count; i++)
                    {
                        if (Manufacturers[i].IdManafacturer == ChangedProduct.Product.Manufacturer.IdManafacturer)
                        {
                            index = i; break;
                        }
                    }
                    SelectedManufacturer = Manufacturers[index];


                    index = 0;
                    for (int i = 0; i < Deliveries.Count; i++)
                    {
                        if (Deliveries[i].IdProvider == ChangedProduct.Product.Delivery.IdProvider)
                        {
                            index = i; break;
                        }
                    }
                    SelectedDelivery = Deliveries[index];


                    index = 0;
                    for (int i = 0; i < Units.Count; i++)
                    {
                        if (Units[i].IdUnit == ChangedProduct.Product.Unit.IdUnit)
                        {
                            index = i; break;
                        }
                    }
                    SelectedUnit = Units[index];

                    Title = ChangedProduct.Product.Title;
                    Description = ChangedProduct.Product.Description;
                    Price = ChangedProduct.Product.Price;
                    CurrentDiscount = ChangedProduct.Product.CurrentDiscount;
                    MaxDiscount = ChangedProduct.Product.MaxDiscount;
                    MaxCount = ChangedProduct.Product.MaxQuantity;
                    SelectedPath = ChangedProduct.Product.Image;
                    ImagePath = new(new Uri(Path.GetFullPath($"Resources/{SelectedPath}"), UriKind.Absolute));
                }
            }
            catch (Exception ex)
            {

            }
        }


        [RelayCommand]
        private void CahngePhoto()
        {
            SelectedPath = _saveFileDialogService.SaveFileDialog();
            ImagePath = new(new Uri(Path.GetFullPath($"Resources/{SelectedPath}"), UriKind.Absolute));
            Debug.WriteLine(SelectedPath);
        }

        [RelayCommand]
        private async void ChangeProduct()
        {
            ValidateAllProperties();

            if (HasErrors == false)
            {

                _productService.ChangeProduct(new ProductDB
                {
                    ProductArticleNumber = ChangedProduct.Product.Article,
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
                ChangedProduct.Product = null;

                _pageService.ChangePage(new ProductsPage());
            }
        }
    }
}
