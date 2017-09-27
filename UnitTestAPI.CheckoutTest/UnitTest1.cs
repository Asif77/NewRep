using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using API.Client;
using API.CheckoutTest.ClientModels.ShoppingBasket;
using API.CheckoutTest.ClientModels;

namespace UnitTestAPI.CheckoutTest
{    
    public class UnitTest1
    {
        private const string Customer1 = "Customer1";
        private const string Customer2 = "Customer2";

        [Fact]
        public void TestShoppingBasketAddItem_EmptyCustomer()
        {
            ShoppingBasketClient client = new ShoppingBasketClient();

            var response = client.AddItem(new AddItemRequest()
            {
                CustomerId = "",
                ProductId = 1,
                Quantity = 1
            });

            Assert.Equal(response.Status, Status.Fail);
        }

        [Fact]
        public void TestShoppingBasketAddItem_InvalidProduct()
        {
            ShoppingBasketClient client = new ShoppingBasketClient();

            var response = client.AddItem(new AddItemRequest()
            {
                CustomerId = Customer1,
                ProductId = 100,
                Quantity = 1
            });

            Assert.Equal(response.Status, Status.Fail);
        }

        [Fact]
        public void TestShoppingBasketAddItem_AddItems()
        {
            ShoppingBasketClient client = new ShoppingBasketClient();

            var response = client.Clear(Customer1);

            Assert.Equal(response.Status, Status.Success);

            response = client.AddItem(new AddItemRequest()
            {
                CustomerId = Customer1,
                ProductId = 1,
                Quantity = 1
            });

            Assert.Equal(response.Status, Status.Success);

            response = client.AddItem(new AddItemRequest()
            {
                CustomerId = Customer1,
                ProductId = 2,
                Quantity = 10
            });

            Assert.Equal(response.Status, Status.Success);

            GetItemsResponse itemsResponse = client.GetItems(Customer1);

            Assert.Equal(itemsResponse.Status, Status.Success);

            Assert.Equal(itemsResponse.Items.Count, 2);
        }

        [Fact]
        public void TestShoppingBasketAddItem_UpdateItems()
        {
            ShoppingBasketClient client = new ShoppingBasketClient();

            var response = client.Clear(Customer1);

            Additems(Customer1);

            var itemsResponse = client.GetItems(Customer1);

            Assert.Equal(response.Status, Status.Success);

            UpdateItemsRequest updateItemsRequest = new UpdateItemsRequest();
            updateItemsRequest.CustomerId = Customer1;
            updateItemsRequest.Items.Add(new UpdateShoppingBasketItem()
            {
                Id = itemsResponse.Items[0].Id,
                ProductId = itemsResponse.Items[0].ProductId,
                Quantity = 5
            });

            response = client.UpdateItems(updateItemsRequest);

            Assert.Equal(itemsResponse.Status, Status.Success);

            itemsResponse = client.GetItems(Customer1);

            Assert.Equal(itemsResponse.Status, Status.Success);

            Assert.Equal(itemsResponse.Items[0].Quantity, 5);
        }

        [Fact]
        public void TestShoppingBasketAddItem_ForDifferent_Customers()
        {
            ShoppingBasketClient client = new ShoppingBasketClient();

            var response = client.Clear(Customer1);
            response = client.Clear(Customer2);

            Additems(Customer1);

            Additems(Customer2);

            var itemsResponseCustomer1 = client.GetItems(Customer1);

            var itemsResponseCustomer2 = client.GetItems(Customer2);

            Assert.Equal(itemsResponseCustomer1.Items.Count, 2);

            Assert.Equal(itemsResponseCustomer2.Items.Count, 2);
        }

        [Fact]
        public void TestShoppingBasket_Clear()
        {
            ShoppingBasketClient client = new ShoppingBasketClient();

            Additems(Customer1);

            var itemsResponseCustomer1 = client.GetItems(Customer1);

            Assert.Equal(itemsResponseCustomer1.Items.Count, 2);

            var response = client.Clear(Customer1);

            itemsResponseCustomer1 = client.GetItems(Customer1);


            Assert.Equal(itemsResponseCustomer1.Items.Count, 0);
        }

        void Additems(string CustomerId)
        {
            ShoppingBasketClient client = new ShoppingBasketClient();

            Response response = client.AddItem(new AddItemRequest()
            {
                CustomerId = CustomerId,
                ProductId = 1,
                Quantity = 1
            });

            Assert.Equal(response.Status, Status.Success);

            System.Diagnostics.Trace.WriteLine("Start: " + response.StartTime + " endtime: " + response.EndTime);

            response = client.AddItem(new AddItemRequest()
            {
                CustomerId = CustomerId,
                ProductId = 2,
                Quantity = 10
            });

            System.Diagnostics.Trace.WriteLine("Start: " + response.StartTime + " endtime: " + response.EndTime);
        }
      
    }
}
