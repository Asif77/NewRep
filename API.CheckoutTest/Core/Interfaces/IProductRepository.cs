using API.CheckoutTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CheckoutTest.Core.Interfaces
{
    public interface IProductRepository
    {
        int Count();
        void AddProduct(Product product);
        Product FindById(int productId);
    }
}
