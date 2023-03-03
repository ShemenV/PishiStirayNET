using System;
using System.Collections.Generic;

namespace PishiStirayNET;

public partial class OrderStatus
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Order1> Order1s { get; } = new List<Order1>();
}
