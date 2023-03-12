using System.IO;

namespace PishiStirayNET.Models
{
    public class Product
    {
        public string? Article { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public float? Price { get; set; }

        public float? CurrentDiscount { get; set; }



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

    }
}
