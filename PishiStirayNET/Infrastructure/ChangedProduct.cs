using CommunityToolkit.Mvvm.ComponentModel;
using PishiStirayNET.Models;

namespace PishiStirayNET.Infrastructure
{
    public partial class ChangedProduct : ObservableObject
    {
        public static Product Product { get; set; } = null;
    }
}
