using API.CheckoutTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CheckoutTest.Core.Interfaces
{
    public interface IShoppingBasketRepository
    {
        Task<ShoppingBasket> Add(string customerId);
        Task<ShoppingBasket> GetByCustomerId(string customerId);
    }
}
