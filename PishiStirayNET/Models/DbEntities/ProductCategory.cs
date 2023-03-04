using System.Collections.Generic;

namespace PishiStirayNET;

public partial class ProductCategory
{
    public int IdCategory { get; set; }

    public string NameCategory { get; set; } = null!;

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
