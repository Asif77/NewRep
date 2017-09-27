using API.CheckoutTest.ClientModels;
using API.CheckoutTest.ClientModels.ShoppingBasket;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Client
{
    public interface IShoppingBasketClient
    {
        Response AddItem(AddItemRequest request);

        Response RemoveItem(RemoveItemRequest request);

        Response UpdateItems(UpdateItemsRequest request);
        GetItemsResponse GetItems(string customerId);
    }
}
