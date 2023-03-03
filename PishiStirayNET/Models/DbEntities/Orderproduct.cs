using System;
using System.Collections.Generic;

namespace PishiStirayNET;

public partial class Orderproduct
{
    public int OrderId { get; set; }

    public string ProductArticleNumber { get; set; } = null!;

    public int Count { get; set; }

    public virtual Order1 Order { get; set; } = null!;

    public virtual ProductDB ProductArticleNumberNavigation { get; set; } = null!;
}
