using API.CheckoutTest.Core.Interfaces;
using API.CheckoutTest.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Client.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly ConcurrentDictionary<int, Product> _product;
        public ProductRepository()
        {
            _product = new ConcurrentDictionary<int, Product>();
        }
        public void AddProduct(Product product)
        {
            _product.AddOrUpdate(product.ProductId, product, (key, existingVal) => product);
        }

        public int Count()
        {
            return _product.Values.Count;
        }

        public Product FindById(int productId)
        {
            Product product = null;

            return _product.TryGetValue(productId, out product) ? product : null;
        }
    }
}
