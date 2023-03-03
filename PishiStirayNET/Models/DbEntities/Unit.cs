using System;
using System.Collections.Generic;

namespace PishiStirayNET;

public partial class Unit
{
    public int IdUnit { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
