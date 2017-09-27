using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CheckoutTest.Models
{
    public class Product
    {
        public Product()
        {
            
        }
        
        public int ProductId { get; set; }
        
        public string Sku { get; set; }
        
        public string Name { get; set; }

        public decimal Price { get; set; }
        
        public int? Quantity { get; set; }       
    }
}
