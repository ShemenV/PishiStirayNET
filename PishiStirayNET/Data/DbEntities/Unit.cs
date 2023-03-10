using System.Collections.Generic;

namespace PishiStirayNET.Data.DbEntities;

public partial class Unit
{
    public int IdUnit { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
