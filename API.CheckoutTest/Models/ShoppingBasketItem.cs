using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CheckoutTest.Models
{
    public class ShoppingBasketItem
    {
        public ShoppingBasketItem()
        {
            Id = DateTime.Now.Ticks;
        }
        public long Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
