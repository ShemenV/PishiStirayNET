using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PishiStirayNET;

public partial class Issuepoint
{
    [Key]
    public int IdPunkta { get; set; }

    public int PunktIndex { get; set; }

    public string PunktCity { get; set; } = null!;

    public string PunktStreet { get; set; } = null!;

    public int? PunktDom { get; set; }

    public virtual ICollection<Order1> Order1s { get; } = new List<Order1>();
}
