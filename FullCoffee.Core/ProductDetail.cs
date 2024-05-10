using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCoffee.Core
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public int FavoryCount { get; set; }
        public string Description { get; set; }
        public int ReviewCount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
