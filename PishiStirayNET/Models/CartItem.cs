using CommunityToolkit.Mvvm.ComponentModel;
using System.Security.Policy;

namespace PishiStirayNET.Models
{
    public partial class CartItem : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Cost))]
        private Product product;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Cost))]
        private int count;

        public float? Cost => Product?.Price * Count;

        public float? Discount => Product?.Price * Count - Product?.NewPrice * Count;

    }
}
