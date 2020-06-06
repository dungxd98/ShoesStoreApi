using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStoreApi.Models
{
    public class OrderInfo
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public List<OrderDetailsInfo> orderDetailsInfo { get; set; }
    }
}
