using PishiStirayNET.Data.DbEntities;
using System.IO;

namespace PishiStirayNET.Models
{
    public class Product : ProductDB
    {
        public int Count { get; set; }
        public string Image { get; set; }


        public string ImageUrl
        {
            get
            {
                return Path.GetFullPath(@$"Resources\{Image}");
            }
        }


        public float? NewPrice
        {
            get
            {
                if (CurrentDiscount != 0)
                {
                    return (float?)((float)ProductCost - ((float)ProductCost * ((float?)CurrentDiscount / 100)));
                }
                return 0;
            }
        }

        public bool HaveDiscount
        {
            get
            {
                return NewPrice != null;
            }
        }


    }
}
