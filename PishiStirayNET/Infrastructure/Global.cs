using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Models;
using System.Collections.ObjectModel;

namespace PishiStirayNET.Infrastructure
{
    public static class Global
    {
        public static UserDB User { get; set; }
        public static Product Product { get; set; } = null;
        public static ObservableCollection<CartItem> CartProductList { get; set; } = new();
        public static Order Order { get; set; } = new();
    }
}
