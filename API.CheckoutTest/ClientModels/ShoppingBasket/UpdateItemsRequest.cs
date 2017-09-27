using API.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace API.CheckoutTest.ClientModels.ShoppingBasket
{
    public class UpdateItemsRequest : RequestBase
    {
        public UpdateItemsRequest()
        {
            Items = new List<UpdateShoppingBasketItem>();
        }
        public List<UpdateShoppingBasketItem> Items { get; set; }       
    }
}
