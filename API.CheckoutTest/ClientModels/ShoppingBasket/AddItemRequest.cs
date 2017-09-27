using System;
using System.Collections.Generic;
using System.Text;

namespace API.CheckoutTest.ClientModels.ShoppingBasket
{
    public class AddItemRequest : RequestBase
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
