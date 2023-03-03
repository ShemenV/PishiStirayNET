using System;
using System.Collections.Generic;

namespace PishiStirayNET;

public partial class UnitOfMeasurement
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
