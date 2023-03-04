using System.IO;

namespace PishiStirayNET.Models
{
    public class Product
    {
        public string? Article { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public decimal Price { get; set; }
        public float? CurrentDiscount { get; set; }

        private string _image;

        public string Image
        {
            get
            {
                return _image;
            }
            set
            {

                if (value == null || string.IsNullOrWhiteSpace(value))
                {
                    _image = "picture.png";
                    return;
                }
                _image = value;
            }
        }



        public string ImageUrl
        {
            get
            {
                return Path.GetFullPath(@$"Resources\{Image}");
            }
        }


    }
}
