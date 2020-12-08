using Lab3Pizzerian.Enumerations;
using Lab3Pizzerian.Extensions;
using Lab3Pizzerian.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
            }
            else
            {
                return new ConflictObjectResult($"OrderId: {order.ID} already exists.");
            }
        }

        [Route("Orders")]
        [HttpGet]
        public IActionResult GetOrders()
        {
            MockDb instance = MockDb.GetDbInstance();
            var orders = instance.GetOrders();
            if (orders != null)
            {
                var orderMenuModel = new List<OrderMenuModel>();
                foreach (var order in orders)
                {
                    orderMenuModel.Add(new OrderMenuModel
                    {
                        ID = order.ID.ToString(),
                        Food = order.Pizzas.Select(i => i.Name).ToList(),
                        OrderStatus = order.OrderStatus.Description()
                    });
                }
                return new OkObjectResult(orderMenuModel);
            }
            else
            {
                return new NoContentResult();
            }
        }

        [Route("Order/{OrderId}")]
        [HttpGet]
        public IActionResult GetOrder(string OrderId)
        {
            MockDb instance = MockDb.GetDbInstance();
            var order = instance.GetOrder(OrderId);
            if (order != null)
            {
                return new OkObjectResult(order);
            }
            else
            {
                return new NoContentResult();
            }
        }

        [Route("Orders/{OrderStatus}")]
        [HttpGet]
        public IActionResult GetOrdersByStatus(string OrderStatus)
        {
            MockDb instance = MockDb.GetDbInstance();
            var orders = instance.GetOrders((EnumStatus)Enum.Parse(typeof(EnumStatus), OrderStatus));
            if (orders != null)
            {
                var orderMenuModel = new List<OrderMenuModel>();
                foreach (var order in orders)
                {
                    orderMenuModel.Add(new OrderMenuModel
                    {
                        ID = order.ID.ToString(),
                        Food = order.Pizzas.Select(i => i.Name).ToList(),
                        OrderStatus = order.OrderStatus.Description()
                    });
                }
                return new OkObjectResult(orderMenuModel);
            }
            else
            {
                return new NoContentResult();
            }
        }

        [SwaggerOperation(Summary = "Get current pizza menu")]
        [Route("Menu")]
        [HttpGet]
        public IActionResult GetMenu()
        {
            MockDb instance = MockDb.GetDbInstance();
            var pizzaMenu = instance.GetMenu();
            var pizzaMenuModel = new List<PizzaMenuModel>();
            foreach (var pizza in pizzaMenu)
            {
                pizzaMenuModel.Add(
                    new PizzaMenuModel
                    {
                        Name = pizza.Name,
                        Ingredients = pizza.Standard.Select(x => x.Description()).ToList(),
                        Price = pizza.StandardPrice,
                    }
                    );
            }
            if (pizzaMenuModel != null)
            {
                return new OkObjectResult(pizzaMenuModel);
            }
            else
            {
                return NoContent();
            }
        }
      [SwaggerOperation(Summary = "Get current pizza menu Daniel test prototype")]
      [Route("GetMenuPrototypeTest")]
      [HttpGet]
      public IActionResult GetMenuPrototypeTest()
      {
         MockDb instance = MockDb.GetDbInstance();
         var pizzaMenu = instance.GetMenuPrototypeTest();
         var pizzaMenuModel = new List<PizzaMenuModel>();
         var standardPizzaForCloneing = pizzaMenu[0]; //testrad
         var cloneTest = standardPizzaForCloneing.Clone(); //testrad
         foreach (var pizza in pizzaMenu)
         {
            pizzaMenuModel.Add(
                new PizzaMenuModel
                {
                   Name = pizza.Name,
                   Ingredients = pizza.Standard.Select(x => x.Description()).ToList(),
                   Price = pizza.StandardPrice,
                }
                );
         }
         if (pizzaMenuModel != null)
         {
            return new OkObjectResult(pizzaMenuModel);
         }
         else
         {
            return NoContent();
         }
      }
   }
}
