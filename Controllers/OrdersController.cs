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
    public class OrdersController : ControllerBase
    {
        private readonly ShoesStoreApiContext _context;
        public OrdersController(ShoesStoreApiContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public IActionResult Create( OrderInfo orderInfo)
        {
            try
            {
                Order order = new Order
                {
                    DateTime = DateTime.Now,
                    CustomerName = orderInfo.CustomerName,
                    Address = orderInfo.Address,
                    PhoneNumber = orderInfo.PhoneNumber,
                    Email = orderInfo.Email,
                    UserName =orderInfo.Username
                };
                _context.Orders.Add(order);
                _context.SaveChanges();

                foreach (OrderDetailsInfo orderDetailsInfo in
                    orderInfo.orderDetailsInfo)
                {
                    OrderDetails orderDetails = new OrderDetails
                    {
                        OrderId = order.OrderId,
                        ProductId=orderDetailsInfo.ProductId,
                        Quantity=orderDetailsInfo.Quantity,
                        Price=orderDetailsInfo.Price
                    };
                    _context.OrderDetails.Add(orderDetails);
                    _context.SaveChanges();
                }
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest();
            }

        }
    }
}