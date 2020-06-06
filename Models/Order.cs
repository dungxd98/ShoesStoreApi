using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStoreApi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<OrderDetails> OrderLines { get; set; }
        public string UserId { get; set; } 
        public string CustomerName { get; set; }
        public DateTime DateTime { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public decimal OrderTotal { get; set; }

    }
}
