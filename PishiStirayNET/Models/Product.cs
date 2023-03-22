using PishiStirayNET.Data.DbEntities;
using System.IO;

namespace PishiStirayNET.Models
{
    public class Product
    {
        public string? Article { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ManufacturerName { get; set; }
        public float? Price { get; set; }
        public int? MaxQuantity { get; set; }
        public float? CurrentDiscount { get; set; }
        public ProductCategory Category { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Delivery Delivery { get; set; }
        public Unit Unit { get; set; }
        public int? MaxDiscount { get; set; }
        public int IsDeleted { get; set; }


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
                    return Price - (Price * (CurrentDiscount / 100));
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

        public int? ProductQuantityInStock { get; internal set; }
    }
}
