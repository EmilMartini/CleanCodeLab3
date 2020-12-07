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
				return new OkObjectResult(orders);
			}
			else
			{
				return new NoContentResult();
			}
		}

		[Route("Orders/{OrderId}")]
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

		[Route("Orders/{OrderStatus:int}")]
		[HttpGet]
		public IActionResult GetOrdersByStatus(int OrderStatus)
		{
			MockDb instance = MockDb.GetDbInstance();
			var order = instance.GetOrders((EnumStatus)OrderStatus);
			if (order != null)
			{
				return new OkObjectResult(order);
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
						Ingredents = pizza.Standard.Select(x => x.Description()).ToList(),
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
