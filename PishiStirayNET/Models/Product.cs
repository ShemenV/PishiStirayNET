using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStirayNET.Models
{
    public class Product
    {
        public string Article { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public float? CurrentDiscount { get; set; }
        public string Image { get; set; }
    }
}
