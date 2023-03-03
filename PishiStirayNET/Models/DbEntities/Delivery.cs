using System;
using System.Collections.Generic;

namespace PishiStirayNET;

public partial class Delivery
{
    public int Iddelivery { get; set; }

    public string? Namedelivery { get; set; }

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
