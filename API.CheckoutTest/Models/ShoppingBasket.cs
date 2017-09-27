using API.CheckoutTest.ClientModels;
using API.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace API.CheckoutTest.Models
{
    public class ShoppingBasket
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        
        private readonly ConcurrentDictionary<long, ShoppingBasketItem> _items;

        public ShoppingBasket()
        {
            _items = new ConcurrentDictionary<long, ShoppingBasketItem>();            
        }
        
        public async Task<List<ShoppingBasketItem>> GetItems()
        {            
            return await Task.Run(() => _items.Values.ToList()); 
        }        

        public async Task<ShoppingBasketItem> AddItem(ShoppingBasketItem item)
        {
            return await Task.Run(() => _items.AddOrUpdate(item.Id, item, (key, existingVal) => item));
        }

        public async Task<bool> UpdateItems(List<UpdateShoppingBasketItem> updatedItems)
        {
            await Task.Run(() =>
            {
                _items.Values.ToList().ForEach(item =>
                {
                    UpdateShoppingBasketItem updatedItem = updatedItems.FirstOrDefault(i => i.Id == item.Id && i.ProductId == item.ProductId);

                    if (updatedItem == null) return;

                    if (updatedItem.Quantity != item.Quantity)
                    {
                        item.Quantity = updatedItem.Quantity;
                    }
                });


                _items.Values.ToList().Where(item => item.Quantity <= 0).ToList().ForEach(item =>
                {
                    ShoppingBasketItem itemOut;
                    _items.TryRemove(item.Id, out itemOut);
                });
            });

            DateUpdated = DateTime.UtcNow;

            return true;
        }

        public async Task<ShoppingBasketItem> FindItemByProductIdAsync(int productId)
        {
            return await Task.Run(() => _items.Values.FirstOrDefault(x => x.ProductId == productId));            
        }

        public ShoppingBasketItem FindItemByProductId(int productId)
        {
            return _items.Values.FirstOrDefault(x => x.ProductId == productId);
        }

        public ShoppingBasketItem FindItemById(long itemId)
        {
            ShoppingBasketItem item = null;

            return _items.TryGetValue(itemId, out item) ? item : null;
        }

        public bool RemoveItem(long itemId)
        {
            ShoppingBasketItem item = null;

            return _items.TryRemove(itemId, out item);
        }        

        public void Clear()
        {
            _items.Clear();
        }
    }
}
