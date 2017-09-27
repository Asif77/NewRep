
using API.CheckoutTest.ClientModels;
using API.CheckoutTest.ClientModels.ShoppingBasket;
using API.CheckoutTest.Controllers;
using API.CheckoutTest.Core.Interfaces;
using API.CheckoutTest.Models;
using API.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestAPI
{
    public class ShoppingBasketControllerTest
    {
        private const string _customer = "Customer1";
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IShoppingBasketRepository> _mockShoppingBasketRepository;
        public ShoppingBasketControllerTest()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductRepository
                .Setup(x => x.FindById(1))
                .Returns(new Product()
                {
                    ProductId = 1,
                    Name = "Product1",
                    Price = 100,
                    Quantity = 100,
                    Sku = "qwerty"
                });

            _mockShoppingBasketRepository = new Mock<IShoppingBasketRepository>();
            _mockShoppingBasketRepository
                .Setup(x => x.GetByCustomerId(_customer))
                .Returns(Task.FromResult(new ShoppingBasket()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerId = _customer,
                    DateCreated = DateTime.UtcNow
                }));

        }

        [Fact]
        public async Task AddItem_ReturnsResponse()
        {
            // Arrange
            
            var controller = new ShoppingBasketController(_mockShoppingBasketRepository.Object, _mockProductRepository.Object);

            // Act
            var result = await controller.AddItem(new AddItemRequest()
            {
                CustomerId = _customer,
                ProductId = 1,
                Quantity = 1
            });

            // Assert
            var viewResult = Assert.IsType<ObjectResult>(result);
            var response = Assert.IsAssignableFrom<Response>(viewResult.Value);
            Assert.Equal(Status.Success, response.Status);
        }

        [Fact]
        public async Task GetItems_ReturnsResponse()
        {
            // Arrange
            var controller = new ShoppingBasketController(_mockShoppingBasketRepository.Object, _mockProductRepository.Object);


            // Act
            var result = controller.Clear(_customer);
            var resultAddItem = await controller.AddItem(new AddItemRequest()
            {
                CustomerId = _customer,
                ProductId = 1,
                Quantity = 1
            });
            var resultGetItems = await controller.GetItems(_customer);

            // Assert
            var viewResult = Assert.IsType<ObjectResult>(resultGetItems);
            var response = Assert.IsAssignableFrom<GetItemsResponse>(viewResult.Value);
            Assert.Equal(Status.Success, response.Status);
            Assert.Equal(1, response.Items.Count);
        }

        [Fact]
        public async Task AddItem_ReturnsBadRequestResult()
        {
            // Arrange           
            var controller = new ShoppingBasketController(_mockShoppingBasketRepository.Object, _mockProductRepository.Object);
            controller.ModelState.AddModelError("CustomerId", "Required");

            // Act
            var result = await controller.AddItem(new AddItemRequest() { CustomerId = "", ProductId = 1, Quantity = 1 });

            // Assert
            var viewResult = Assert.IsType<ObjectResult>(result);
            var response = Assert.IsAssignableFrom<Response>(viewResult.Value);
            Assert.Equal(Status.Fail, response.Status);
        }

        [Fact]
        public async Task RemoveItem_ReturnResponse()
        {
            // Arrange
            var controller = new ShoppingBasketController(_mockShoppingBasketRepository.Object, _mockProductRepository.Object);


            // Act
            var result = controller.Clear(_customer);
            var resultAddItem = await controller.AddItem(new AddItemRequest()
            {
                CustomerId = _customer,
                ProductId = 1,
                Quantity = 1
            });
            var resultGetItems = await controller.GetItems(_customer);
            

            // Assert
            var response = Assert.IsAssignableFrom<GetItemsResponse>(Assert.IsType<ObjectResult>(resultGetItems).Value);
            var resultRemoveItem = controller.RemoveItem(new RemoveItemRequest() { CustomerId = _customer, Id = response.Items[0].Id });
            resultGetItems = await controller.GetItems(_customer);
            var responseGetItems = Assert.IsAssignableFrom<GetItemsResponse>(Assert.IsType<ObjectResult>(resultGetItems).Value);
            Assert.Equal(0, responseGetItems.Items.Count);
        }
    }
}
