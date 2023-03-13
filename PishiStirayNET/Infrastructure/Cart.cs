using PishiStirayNET.Models;
using System.Collections.ObjectModel;

namespace PishiStirayNET.Infrastructure
{
    public static class Cart
    {
        public static ObservableCollection<CartItem> CartProductList { get; set; } = new();
    }
}
