using CommunityToolkit.Mvvm.ComponentModel;

namespace PishiStirayNET.Models
{
    public partial class CartItem: ObservableObject
    {
        public Product Product { get; set; }

        [ObservableProperty]
        private int count;
    }
}
