using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PishiStirayNET;

public partial class ProductDB
{
    [Key] 
    public string ProductArticleNumber { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public int ProductCategory { get; set; }

    public string? ProductPhoto { get; set; }

    public int ProductManufacturer { get; set; }

    public decimal ProductCost { get; set; }

    public sbyte? CurrentDiscount { get; set; }

    public int ProductQuantityInStock { get; set; }

    public int UnitOfMeasurement { get; set; }

    public int Delivery { get; set; }

    public int? ProductDiscountAmount { get; set; }

    public virtual Delivery DeliveryNavigation { get; set; } = null!;

    public virtual ICollection<Orderproduct> Orderproducts { get; } = new List<Orderproduct>();

    public virtual ProductCategory ProductCategoryNavigation { get; set; } = null!;

    public virtual Manufacturer ProductManufacturerNavigation { get; set; } = null!;

    public virtual Unit UnitOfMeasurementNavigation { get; set; } = null!;
}
