using CommunityToolkit.Mvvm.ComponentModel;

namespace PishiStirayNET.Models
{
    public partial class CartItem : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Cost))]
        private Product product;

        [ObservableProperty]
        private int count;

        public float? Cost => (float?)(Product?.ProductCost * Count);

        public float? Discount => (float?)(Product?.ProductCost * Count) - Product?.NewPrice * Count;

    }
}
