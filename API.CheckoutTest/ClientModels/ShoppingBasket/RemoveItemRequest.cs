using System;
using System.Collections.Generic;
using System.Text;

namespace API.CheckoutTest.ClientModels.ShoppingBasket
{
    public class RemoveItemRequest : RequestBase
    {
        public long Id { get; set; }
    }
}
