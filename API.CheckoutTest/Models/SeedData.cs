using API.CheckoutTest.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CheckoutTest.Models
{
    public class SeedData
    {
        private IProductRepository _productRep;

        public SeedData(IProductRepository rep)
        {
            _productRep = rep;
        }

        public void EnsureSeedData()
        {
            if (_productRep.Count() == 0)
            {
                _productRep.AddProduct(new Product() { ProductId = 1, Name = "Product1", Price = 100, Quantity = 100, Sku = "qwerty" });
                _productRep.AddProduct(new Product() { ProductId = 2, Name = "Product2", Price = 100, Quantity = 200, Sku = "qwerty" });
                _productRep.AddProduct(new Product() { ProductId = 3, Name = "Product3", Price = 100, Quantity = 300, Sku = "qwerty" });
                _productRep.AddProduct(new Product() { ProductId = 4, Name = "Product4", Price = 100, Quantity = 400, Sku = "qwerty" });
                _productRep.AddProduct(new Product() { ProductId = 5, Name = "Product5", Price = 100, Quantity = 500, Sku = "qwerty" });
            }
        }
    }
}
