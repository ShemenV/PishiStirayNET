using System.Collections.Generic;

namespace PishiStirayNET.Data.DbEntities;

public partial class Delivery
{
    public int IdProvider { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
