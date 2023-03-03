using System;
using System.Collections.Generic;

namespace PishiStirayNET;

public partial class ProductDB
{
    public string ProductArticleNumber { get; set; } = null!;

    public int ProductName { get; set; }

    public string ProductDescription { get; set; } = null!;

    public int ProductCategory { get; set; }

    public string ProductPhoto { get; set; } = null!;

    public int ProductManufacturer { get; set; }

    public decimal ProductCost { get; set; }

    public sbyte? ProductDiscountAmount { get; set; }

    public int ProductQuantityInStock { get; set; }

    public int? UnitOfMeasurement { get; set; }

    public int? Delivery { get; set; }

    public float? CurrentDiscount { get; set; }

    public virtual Delivery? DeliveryNavigation { get; set; }

    public virtual ICollection<Orderproduct> Orderproducts { get; } = new List<Orderproduct>();

    public virtual Category ProductCategoryNavigation { get; set; } = null!;

    public virtual Manufacturer ProductManufacturerNavigation { get; set; } = null!;

    public virtual Name ProductNameNavigation { get; set; } = null!;

    public virtual UnitOfMeasurement? UnitOfMeasurementNavigation { get; set; }
}
