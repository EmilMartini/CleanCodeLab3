using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Controllers
{
    [Route("Api")]
    public class ApiController : Controller
    {
        [Route("Order")]
        [HttpPost]
        public IActionResult CreateOrder()
        {
            MockDb instance = MockDb.GetDbInstance();
            var order = new Order();
            order.ID = Guid.NewGuid();
            var accepted = instance.CreateOrder(order);
            if (accepted)
            {
                return new OkObjectResult($"Order created with OrderId: {order.ID}{Environment.NewLine}" +
                    $"Save this OrderId to add food and drinks to your order.");
            } else
            {
                return new ConflictObjectResult($"OrderId: {order.ID} already exists.");
            }
        }

        [Route("Order")]
        [HttpGet]
        public IActionResult GetOrder()
        {
            MockDb instance = MockDb.GetDbInstance();
            var orders = instance.GetOrders();
            if(orders != null)
            {
                return new OkObjectResult(orders);
            } else
            {
                return new NoContentResult();
            }
        }

        [Route("Order/{OrderId}")]
        [HttpGet]
        public IActionResult GetOrder(string orderId)
        {
            MockDb instance = MockDb.GetDbInstance();
            var order = instance.GetOrder(orderId);
            if (order != null)
            {
                return new OkObjectResult(order);
            }
            else
            {
                return new NoContentResult();
            }
        }

        [Route("Menu")]
        [HttpGet]
        public IActionResult GetMenu()
        {
            MockDb instance = MockDb.GetDbInstance();
            var pizzas = instance.GetMenu();
            if(pizzas != null)
            {
                return new OkObjectResult(pizzas);
            } else
            {
                return NoContent();
            }
        }    
    }
}
