using System;
using System.Collections.Generic;
using System.Text;

namespace API.CheckoutTest.ClientModels
{   
    public class UpdateShoppingBasketItem
    {
        public long Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
