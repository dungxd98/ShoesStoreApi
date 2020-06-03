using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStoreApi.Data;
using ShoesStoreApi.Models;

namespace ShoesStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly Cart _cart;
        private readonly ShoesStoreApiContext _contextProduct;
        public CartController(ShoesStoreApiContext contextProduct, Cart cart)
        {
            _cart = cart;
            _contextProduct = contextProduct;
        }

        //GET /api/cart 

        [HttpGet()]
        public IActionResult GetShoppingCartItems()
        {
            var items = _cart.GetShoppingCartItems();
            _cart.CartItems = items;

            var cartModel = new CartModel
            {
                Cart = _cart,
                CartTotal = _cart.GetCartTotal()
            };
            return Ok(cartModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            try
            {
                    var selectedProduct = await _contextProduct.Products.FindAsync(productId);

                    if (selectedProduct != null)
                {
                    _cart.AddToCart(selectedProduct, 1);
                }
                else
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var selectedProduct = await _contextProduct.Products.FindAsync(productId);

            if(selectedProduct != null)
            {
                _cart.RemoveFromCart(selectedProduct);
            }

            return Ok();
        }
    }
}