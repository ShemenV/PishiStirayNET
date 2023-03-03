using System;
using System.Collections.Generic;

namespace PishiStirayNET;

public partial class Name
{
    public int Idnames { get; set; }

    public string? Names { get; set; }

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
