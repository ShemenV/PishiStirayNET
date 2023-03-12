using CommunityToolkit.Mvvm.ComponentModel;

namespace PishiStirayNET.Models
{
    public partial class CartItem : ObservableObject
    {
        [ObservableProperty]
        private Product product;

        [ObservableProperty]
        private int count;
    }
}
