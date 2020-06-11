using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet]
        [Route("GetOrder")]
        //GET : /api/orders/GetOrder
        public IActionResult GetOrder()
        {
            try
            {
                var order = _context.Orders.ToList();

                return Ok(order);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        //[Route("GetOrderByUserName")]
        //GET : /api/orders?userName=admin
        public IActionResult GetOrderByUserName(string userName)
        {
            try
            {
                var order = _context.Orders.Where(c => c.UserName == userName)
               .ToList();

                return Ok(order);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetOrderById")]
        //GET : /api/orders/GetOrderById?id=5
        public IActionResult GetOrderById(int id)
        {
            try
            {
                var order = _context.Orders.Where(c => c.Id == id)
               .ToList();

                return Ok(order);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        //GET : /api/orders/1
        public IActionResult GetOrderByOrderId(int id)
        {
            try
            {
                var order = _context.OrderDetails.Where(c => c.OrderId == id)
               .ToList();

                return Ok(order);
            }
            catch
            {
                return BadRequest();
            }
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
                    UserName = orderInfo.Username,
                    State = orderInfo.State,
                    OrderTotal = orderInfo.OrderTotal.ToString()
                };
                _context.Orders.Add(order);
                _context.SaveChanges();

                foreach (OrderDetailsInfo orderDetailsInfo in
                    orderInfo.orderDetailsInfo)
                {
                    OrderDetails orderDetails = new OrderDetails
                    {
                        OrderId = order.Id,
                        ProductId=orderDetailsInfo.ProductId,
                        ProductName=orderDetailsInfo.ProductName,
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
        [HttpPost]
        [Route("UpdateStateDone")]
        //POST : /api/orders/UpdateStateDone
        public async Task<IActionResult> UpdateStatusDeActive(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order.State == "pending")
            {
                order.State = "Done";
            }
            else
            {
                return BadRequest(new { message = "Đã thanh toán rồi." });
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}