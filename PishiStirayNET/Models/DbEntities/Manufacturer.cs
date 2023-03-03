using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PishiStirayNET;

public partial class Manufacturer
{
    [Key]
    public int IdManafacturer { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
