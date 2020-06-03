using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStoreApi.Models
{
    public class CartModel
    {
        public Cart Cart { get; set; }
        public decimal CartTotal { get; set; }
    }
}
