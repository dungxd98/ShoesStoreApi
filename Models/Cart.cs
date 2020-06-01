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
            Id = buyerId;
        }

        public string Id { get; set; }
        public string UserId { get; set; }

        public IEnumerable<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
