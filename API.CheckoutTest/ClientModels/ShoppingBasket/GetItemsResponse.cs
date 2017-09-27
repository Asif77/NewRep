using API.CheckoutTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.CheckoutTest.ClientModels.ShoppingBasket
{
    public class GetItemsResponse : Response
    {
        public GetItemsResponse()
        {
            Items = new List<ShoppingBasketItem>();
        }
        public List<ShoppingBasketItem> Items { get; set; }
        
    }
}
