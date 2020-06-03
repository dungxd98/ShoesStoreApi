using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoesStoreApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStoreApi.Models
{
    public class Cart
    {
        public Cart() { }

        public Cart(string buyerId)
        {
            CartId = buyerId;
        }
        private readonly ShoesStoreApiContext _context;
        public Cart(ShoesStoreApiContext context)
        {
            _context = context;
        }

        public string CartId { get; set; }

        public List<CartItem> CartItems { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ShoesStoreApiContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new Cart(context) { CartId = cartId };
        }
        public void AddToCart(Product product, int quantity)
        {
            var shoppingCartItem = _context.CartItems.SingleOrDefault(s => s.Product.Id == product.Id && s.CartId == CartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new CartItem
                {
                    CartId = CartId,
                    Product = product,
                    Quantity = quantity
                };
                _context.CartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity++;
            }
            _context.SaveChanges();
        }
        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = _context.CartItems.SingleOrDefault(s => s.Product.Id == product.Id && s.CartId == CartId);

            var localQuantity = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                    localQuantity = shoppingCartItem.Quantity;
                }
                else
                {
                    _context.CartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
            return localQuantity;
        }
        public List<CartItem> GetShoppingCartItems()
        {
            return CartItems ?? (CartItems = _context.CartItems.Where(c => c.CartId == CartId)
                .Include(s => s.Product)
                .ToList());
        }
        public void ClearCart()
        {
            var cartItems = _context.CartItems.Where(cart => cart.CartId == CartId);

            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }
        public decimal GetCartTotal()
        {
            var total = _context.CartItems.Where(c => c.CartId == CartId).Select(c => Convert.ToInt32(c.Product.Price) * c.Quantity).Sum();
            return total;
        }
    }
}
