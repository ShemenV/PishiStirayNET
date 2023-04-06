using CommunityToolkit.Mvvm.ComponentModel;
using PishiStirayNET.Data.DbEntities;
using System.Collections.Generic;

namespace PishiStirayNET.Models
{
    public partial class Order : Order1
    {
        [ObservableProperty]
        private float fullPrice;

        [ObservableProperty]
        private float discount;

        [ObservableProperty]
        private List<int> productQuatities;
    }
}
