using System;
using System.Collections.Generic;

namespace PishiStirayNET;

public partial class Issuepoint
{
    public int Id { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public int? House { get; set; }

    public int? Index { get; set; }

    public virtual ICollection<Order1> Order1s { get; } = new List<Order1>();
}
