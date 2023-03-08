using PishiStirayNET.Models;
using System.Collections.Generic;

namespace PishiStirayNET.Infrastructure
{
    public class Cart
    {
        public static List<CartItem> CartProductList { get; set; } = new();
    }
}
