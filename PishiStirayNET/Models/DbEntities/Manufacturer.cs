using System;
using System.Collections.Generic;

namespace PishiStirayNET;

public partial class Manufacturer
{
    public int IdManufacturers { get; set; }

    public string? ManufacturersName { get; set; }

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
