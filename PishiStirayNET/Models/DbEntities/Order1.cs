using System;
using System.Collections.Generic;

namespace PishiStirayNET;

public partial class Order1
{
    public int OrderId { get; set; }

    public int OrderStatus { get; set; }

    public DateTime OrderDeliveryDate { get; set; }

    public int OrderPickupPoint { get; set; }

    public int? PickUpCode { get; set; }

    public string? ClientName { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual Issuepoint OrderPickupPointNavigation { get; set; } = null!;

    public virtual OrderStatus OrderStatusNavigation { get; set; } = null!;

    public virtual ICollection<Orderproduct> Orderproducts { get; } = new List<Orderproduct>();
}
