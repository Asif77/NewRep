using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Client;
using API.Client.Models;
using API.CheckoutTest.Core.Interfaces;
using API.CheckoutTest.ClientModels.ShoppingBasket;
using API.CheckoutTest.ClientModels;
using API.CheckoutTest.Models;

namespace API.CheckoutTest.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ShoppingBasketController : Controller
    {
        IShoppingBasketRepository _ShoppingBasketRepository;
        IProductRepository _productRepository;
        public ShoppingBasketController(IShoppingBasketRepository rep, IProductRepository productRep)
        {
            _ShoppingBasketRepository = rep;
            _productRepository = productRep;
        }
        

        [HttpPost]
        [Route("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] AddItemRequest addItemRequest)
        {
            using (Response response = new Response())
            {
                try
                {
                    if (!ModelState.IsValid)
                        throw new Exception("Bad request.");

                    //there could be better validation design and handle custom exception. but for now throw exception
                    if (string.IsNullOrEmpty(addItemRequest.CustomerId))
                        throw new Exception("CustomerId should not be null or empty.");

                    var product = _productRepository.FindById(addItemRequest.ProductId);
                    if (product == null)
                        throw new Exception("Product does not exist.");

                    ShoppingBasket basket = await _ShoppingBasketRepository.GetByCustomerId(addItemRequest.CustomerId);
                    
                    ShoppingBasketItem existingBasketItem = basket.FindItemByProductId(addItemRequest.ProductId);

                    if (existingBasketItem != null)
                    {
                        // Add quantity to existing basket item
                        existingBasketItem.Quantity += addItemRequest.Quantity;
                    }
                    else
                    {
                        // Add item to basket.
                        var basketItem = new ShoppingBasketItem
                        {
                            ProductId = addItemRequest.ProductId,
                            Quantity = addItemRequest.Quantity,
                            Price = product.Price
                        };

                        await basket.AddItem(basketItem);
                    }

                    response.SetSuccessResult("Operation completed successfully.");
                }
                catch (Exception exp)
                {
                    string exceptionMessage = string.Format("Message: {0}, Trace: {1}, InnerException: {2}", exp.Message, exp.StackTrace, exp.InnerException == null ? "" : exp.InnerException.Message);
                    response.SetFailureResult(exceptionMessage);
                }
                finally
                {
                    response.EndTime = DateTime.UtcNow;
                }


                return new ObjectResult(response);
            }
        }

        [HttpPost("{id}")]
        [Route("RemoveItem")]
        public async Task<IActionResult> RemoveItem([FromBody] RemoveItemRequest removeItemRequest)
        {
            using (Response response = new Response())
            {
                try
                {
                    if (!ModelState.IsValid)
                        throw new Exception("Bad request.");

                    if (string.IsNullOrEmpty(removeItemRequest.CustomerId))
                        throw new Exception("CustomerId should not be null or empty.");

                    ShoppingBasket basket = await _ShoppingBasketRepository.GetByCustomerId(removeItemRequest.CustomerId);

                    ShoppingBasketItem item = basket.FindItemById(removeItemRequest.Id);
                    if (item != null)
                    {
                        basket.RemoveItem(removeItemRequest.Id);
                    }
                    else
                        throw new Exception("Invalid basket item.");

                    response.SetSuccessResult("Operation completed successfully.");
                }
                catch (Exception exp)
                {
                    string exceptionMessage = string.Format("Message: {0}, Trace: {1}, InnerException: {2}", exp.Message, exp.StackTrace, exp.InnerException == null ? "" : exp.InnerException.Message);
                    response.SetFailureResult(exceptionMessage);
                }
                finally
                {
                    response.EndTime = DateTime.UtcNow;
                }

                return new ObjectResult(response);
            }
        }

        [HttpPut]
        [Route("UpdateItems")]
        public async Task<ActionResult> UpdateItems([FromBody] UpdateItemsRequest updateItemRequest)
        {
            using (Response response = new Response())
            {
                try
                {
                    if (!ModelState.IsValid)
                        throw new Exception("Bad request.");

                    if (string.IsNullOrEmpty(updateItemRequest.CustomerId))
                        throw new Exception("CustomerId should not be null or empty.");

                    ShoppingBasket basket = await _ShoppingBasketRepository.GetByCustomerId(updateItemRequest.CustomerId);

                    bool result = await basket.UpdateItems(updateItemRequest.Items);

                    response.SetSuccessResult("Operation completed successfully.");
                }
                catch (Exception exp)
                {
                    string exceptionMessage = string.Format("Message: {0}, Trace: {1}, InnerException: {2}", exp.Message, exp.StackTrace, exp.InnerException == null ? "" : exp.InnerException.Message);
                    response.SetFailureResult(exceptionMessage);
                }
                finally
                {
                    response.EndTime = DateTime.UtcNow;
                }

                return new ObjectResult(response);
            }
        }

        [HttpGet]
        [Route("GetItems")]
        public async Task<ActionResult> GetItems(string customerId)
        {
            using (GetItemsResponse response = new GetItemsResponse())
            {
                try
                {
                    if (!ModelState.IsValid)
                        throw new Exception("Bad request.");

                    if (string.IsNullOrEmpty(customerId))
                        throw new Exception("CustomerId should not be null or empty.");

                    ShoppingBasket basket = await _ShoppingBasketRepository.GetByCustomerId(customerId);

                    response.Items.AddRange(await basket.GetItems());

                    response.SetSuccessResult("Operation completed successfully.");
                }
                catch (Exception exp)
                {
                    string exceptionMessage = string.Format("Message: {0}, Trace: {1}, InnerException: {2}", exp.Message, exp.StackTrace, exp.InnerException == null ? "" : exp.InnerException.Message);
                    response.SetFailureResult(exceptionMessage);
                }
                finally
                {
                    response.EndTime = DateTime.UtcNow;
                }

                return new ObjectResult(response);
            }
        }

        [HttpDelete("{id}")]
        [Route("Clear")]
        public async Task<ActionResult> Clear(string customerId)
        {
            using (Response response = new Response())
            {
                try
                {
                    if (!ModelState.IsValid)
                        throw new Exception("Bad request.");

                    if (string.IsNullOrEmpty(customerId))
                        throw new Exception("CustomerId should not be null or empty.");

                    ShoppingBasket basket = await _ShoppingBasketRepository.GetByCustomerId(customerId);
                    
                    basket.Clear();

                    response.SetSuccessResult("Operation completed successfully.");
                }
                catch (Exception exp)
                {
                    string exceptionMessage = string.Format("Message: {0}, Trace: {1}, InnerException: {2}", exp.Message, exp.StackTrace, exp.InnerException == null ? "" : exp.InnerException.Message);
                    response.SetFailureResult(exceptionMessage);
                }
                finally
                {
                    response.EndTime = DateTime.UtcNow;
                }

                return new ObjectResult(response);
            }
        }
    }
}
