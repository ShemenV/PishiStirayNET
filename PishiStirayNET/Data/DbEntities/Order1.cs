using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PishiStirayNET.Data.DbEntities;

public partial class Order1: ObservableObject
{
    [Key]
    public int OrderId { get; set; }

    public int OrderStatus { get; set; }

    public DateTime OrderDeliveryDate { get; set; }

    public DateTime OrderDeliveryDateEnd { get; set; }

    public int OrderPickupPoint { get; set; }

    public string? Fio { get; set; }

    public int CodePoluch { get; set; }

    public virtual Issuepoint OrderPickupPointNavigation { get; set; } = null!;

    public virtual OrderStatus OrderStatusNavigation { get; set; } = null!;

    public virtual ICollection<Orderproduct> Orderproducts { get; } = new List<Orderproduct>();
}
