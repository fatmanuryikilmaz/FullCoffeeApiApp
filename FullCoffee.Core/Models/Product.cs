using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCoffee.Core.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string? OldPrice { get; set; }
        public string? Image { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public ProductDetail ProductDetail { get; set; }
    }
}
