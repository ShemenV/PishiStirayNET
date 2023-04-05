using CommunityToolkit.Mvvm.ComponentModel;
using PishiStirayNET.Data.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStirayNET.Models
{
    public partial class Order : Order1
    {
        [ObservableProperty]
        private float fullPrice;

        [ObservableProperty]
        private float discount;
    }
}
