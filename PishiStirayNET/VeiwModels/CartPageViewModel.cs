using CommunityToolkit.Mvvm.ComponentModel;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Models;
using System.Collections.Generic;

namespace PishiStirayNET.VeiwModels
{
    public partial class CartPageViewModel : ObservableObject
    {

        [ObservableProperty]
        private List<CartItem> cartProductsList;


        public CartPageViewModel()
        {
            cartProductsList = Cart.CartProductList;
        }
    }
}
