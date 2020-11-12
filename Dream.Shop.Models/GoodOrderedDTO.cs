using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.Models
{
    public class GoodOrderedDTO
    {
        public string OrderId { get; set; }
        public string GoodId { get; set; }
        public string GoodName { get; set; }
        public string ImgUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string SpecificationName { get; set; }
    }
}
