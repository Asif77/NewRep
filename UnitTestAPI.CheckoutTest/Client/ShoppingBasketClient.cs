using API.CheckoutTest.ClientModels;
using API.CheckoutTest.ClientModels.ShoppingBasket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace API.Client
{    
    public class ShoppingBasketClient : ClientBase, IShoppingBasketClient
    {
        private const string ShoppingBasketUri = "api/ShoppingBasket/";

        public Response AddItem(AddItemRequest request)
        {
            return HttpPost<Response>(ShoppingBasketUri + "AddItem", request).Result;
        }

        public Response RemoveItem(RemoveItemRequest request)
        {
            var reqJson = JsonConvert.SerializeObject(request);
            return HttpDelete<Response>(ShoppingBasketUri + "RemoveItem", reqJson).Result;
        }

        public Response UpdateItems(UpdateItemsRequest request)
        {
            return HttpPut<Response>(ShoppingBasketUri + "UpdateItems", request).Result;
        }

        public GetItemsResponse GetItems(string customerId)
        {
            return HttpGet<GetItemsResponse>(ShoppingBasketUri + "GetItems", "customerId=" + customerId).Result;
        }

        public Response Clear(string customerId)
        {
            return HttpDelete<Response>(ShoppingBasketUri + "Clear", "customerId=" + customerId).Result;
        }
    }
}
