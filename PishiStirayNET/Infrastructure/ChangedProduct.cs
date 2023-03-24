using CommunityToolkit.Mvvm.ComponentModel;
using PishiStirayNET.Data.DbEntities;
using PishiStirayNET.Models;

namespace PishiStirayNET.Infrastructure
{
    public partial class ChangedObjects : ObservableObject
    {
        public static Product Product { get; set; } = null;
       
    }
}
