using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ShoesStoreApi.Data;
using ShoesStoreApi.Models;

namespace ShoesStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ShoesStoreApiContext _context;
        public OrdersController(ShoesStoreApiContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult CreateProduct(Order order)
        {
            try
            {
                order.DateTime = DateTime.Now;
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            catch
            {

            }

            return Ok();
        }


    }
}