using API.CheckoutTest.Core.Interfaces;
using API.CheckoutTest.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Client.Models
{
    public class ShoppingBasketRepository : IShoppingBasketRepository
    {
        private readonly ConcurrentDictionary<string, ShoppingBasket> _baskets;
        public ShoppingBasketRepository()
        {
            _baskets = new ConcurrentDictionary<string, ShoppingBasket>();
        }
       
        public async Task<ShoppingBasket> Add(string customerId)
        {
            var basket = new ShoppingBasket()
            {
                CustomerId = customerId,
                DateCreated = DateTime.UtcNow,
                Id = System.Guid.NewGuid().ToString()
            };

            return await Task.Run(() => _baskets.AddOrUpdate(basket.CustomerId, basket, (key, existingVal) => basket));
        }

        public async Task<ShoppingBasket> GetByCustomerId(string customerId)
        {
            ShoppingBasket basket = null;

            if (!_baskets.TryGetValue(customerId, out basket) && basket == null)
                basket = await Add(customerId);

            return basket;
        }     
    }
}
