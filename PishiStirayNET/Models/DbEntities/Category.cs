using System;
using System.Collections.Generic;

namespace PishiStirayNET;

public partial class Category
{
    public int Idcategories { get; set; }

    public string? Namecategories { get; set; }

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
