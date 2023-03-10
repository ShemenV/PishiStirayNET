using System.Collections.Generic;

namespace PishiStirayNET.Data.DbEntities;

public partial class OrderStatus
{
    public int IdOrderStatus { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order1> Order1s { get; } = new List<Order1>();
}
