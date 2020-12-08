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
		// en APIcall per enum
		//OpenNewOrder = 1, // 
		//AddPizza = 2,
		//EditPizza = 3, 
		//PlaceOrder = 4,
		//CompleteOrder = 5,
		//CancelOrder = 6,
		//ViewOpenOrder = 7,
		//[SwaggerOperation(Summary = "Creates a new order")]
		//[Route("Order")]
		//[HttpPost]
		//public IActionResult CreateOrder()
		//{
		//	MockDb instance = MockDb.GetDbInstance();
		//	var order = new Order();
		//	order.ID = Guid.NewGuid();
		//	var accepted = instance.CreateOrder(order);
		//	if (accepted)
		//	{
		//		return new OkObjectResult($"Order created with OrderId: {order.ID}{Environment.NewLine}" +
		//			  $"Save this OrderId to add food and drinks to your order.");
		//	}
		//	else
		//	{
		//		return new ConflictObjectResult($"OrderId: {order.ID} already exists.");
		//	}
		//}



		[SwaggerOperation(Summary = "Creates a new order2")]
		[Route("Order2")]
		[HttpPost]
		public IActionResult CreateOrder2()
		{
			MockDb instance = MockDb.GetDbInstance();
			if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.OpenNewOrder) == false)
			{
				return new ConflictObjectResult("You cant open a new order now");
			}
			var order = new Order();
			order.ID = Guid.NewGuid();
			var accepted = instance.CreateOrder2(order);
			if (accepted)
			{
				return new OkObjectResult($"Order created with OrderId: {order.ID}{Environment.NewLine}");
			}
			else
			{
				return new ConflictObjectResult($"OrderId: {order.ID} already exists.");
			}
		}
		[SwaggerOperation(Summary = "Add Pizza to cart")]
		[Route("AddPizza2/{MenuNumber}")]
		[HttpPost]
		public IActionResult AddPizza2(int MenuNumber)
		{
			MockDb instance = MockDb.GetDbInstance();
			if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.AddPizza) == false)
			{
				return new ConflictObjectResult("You cant add a pizza now");
			}
			if (instance.Menu2.Count() < MenuNumber || 0 >= MenuNumber)
			{
				return new ConflictObjectResult("Thats not on the menu");
			}
			var pizzaToAdd = instance.Menu2[MenuNumber - 1].Clone();
			instance.Order.Pizzas.Add((Pizza2)pizzaToAdd);
			return new OkObjectResult($"You have added the pizza {pizzaToAdd.Name} to your order");
		}

		[SwaggerOperation(Summary = "Place Order")]
		[Route("PlaceOrder2")]
		[HttpPost]
		public IActionResult PlaceOrder2()
		{
			MockDb instance = MockDb.GetDbInstance();
			if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.PlaceOrder) == false)
			{
				return new ConflictObjectResult("You cant place your order now");
			}
			instance.Order.OrderStatus = EnumStatus.Placed;
			return new OkObjectResult($"You have placed your order");
		}
		[SwaggerOperation(Summary = "View your current order")]
		[Route("ViewOrder")]
		[HttpGet]
		public IActionResult ViewOrder()
		{
			MockDb instance = MockDb.GetDbInstance();
			if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.ViewOpenOrder) == false)
			{
				return new ConflictObjectResult("You cant view your order now");
			}
			instance.Order.OrderStatus = EnumStatus.Placed;
			return new OkObjectResult(instance.Order);
		}







		// tror detta är fel. efter att ha läst documentationen tror jag att vi ska visa alla som INTE är i orderslistan (känns helknäppt)
		//[Route("Orders")]
		//[HttpGet]
		//public IActionResult GetOrders()
		//{
		//	MockDb instance = MockDb.GetDbInstance();
		//	var orders = instance.GetOrders();
		//	if (orders != null)
		//	{
		//		var orderMenuModel = new List<OrderMenuModel>();
		//		foreach (var order in orders)
		//		{
		//			orderMenuModel.Add(new OrderMenuModel
		//			{
		//				ID = order.ID.ToString(),
		//				Food = order.Pizzas.Select(i => i.Name).ToList(),
		//				OrderStatus = order.OrderStatus.Description()
		//			});
		//		}
		//		return new OkObjectResult(orderMenuModel);
		//	}
		//	else
		//	{
		//		return new NoContentResult();
		//	}
		//}

		//[Route("Order/{OrderId}")]
		//[HttpGet]
		//public IActionResult GetOrder(string OrderId)
		//{
		//	MockDb instance = MockDb.GetDbInstance();
		//	var order = instance.GetOrder(OrderId);
		//	if (order != null)
		//	{
		//		return new OkObjectResult(order);
		//	}
		//	else
		//	{
		//		return new NoContentResult();
		//	}
		//}

		//[Route("Orders/{OrderStatus}")]
		//[HttpGet]
		//public IActionResult GetOrdersByStatus(string OrderStatus)
		//{
		//	MockDb instance = MockDb.GetDbInstance();
		//	var orders = instance.GetOrders((EnumStatus)Enum.Parse(typeof(EnumStatus), OrderStatus));
		//	if (orders != null)
		//	{
		//		var orderMenuModel = new List<OrderMenuModel>();
		//		foreach (var order in orders)
		//		{
		//			orderMenuModel.Add(new OrderMenuModel
		//			{
		//				ID = order.ID.ToString(),
		//				Food = order.Pizzas.Select(i => i.Name).ToList(),
		//				OrderStatus = order.OrderStatus.Description()
		//			});
		//		}
		//		return new OkObjectResult(orderMenuModel);
		//	}
		//	else
		//	{
		//		return new NoContentResult();
		//	}
		//}

		//[SwaggerOperation(Summary = "Get current pizza menu")]
		//[Route("Menu")]
		//[HttpGet]
		//public IActionResult GetMenu()
		//{
		//	MockDb instance = MockDb.GetDbInstance();
		//	var pizzaMenu = instance.GetMenu();
		//	var pizzaMenuModel = new List<PizzaMenuModel>();
		//	foreach (var pizza in pizzaMenu)
		//	{
		//		pizzaMenuModel.Add(
		//			 new PizzaMenuModel
		//			 {
		//				 Name = pizza.Name,
		//				 Ingredients = pizza.Standard.Select(x => x.Description()).ToList(),
		//				 Price = pizza.StandardPrice,
		//			 }
		//			 );
		//	}
		//	if (pizzaMenuModel != null)
		//	{
		//		return new OkObjectResult(pizzaMenuModel);
		//	}
		//	else
		//	{
		//		return NoContent();
		//	}
		//}
		[SwaggerOperation(Summary = "Get current pizza menu")]
		[Route("GetMenuPrototypeTest")]
		[HttpGet]
		public IActionResult GetMenuPrototypeTest()
		{
			MockDb instance = MockDb.GetDbInstance();
			var pizzaMenu = instance.GetMenuPrototypeTest();
			var pizzaMenuModel = new List<PizzaMenuModel>();
			var standardPizzaForCloneing = pizzaMenu[0]; //testrad
			var cloneTest = standardPizzaForCloneing.Clone(); //testrad
			int menuNumber = 1;
			foreach (var pizza in pizzaMenu)
			{
				pizzaMenuModel.Add(
					 new PizzaMenuModel
					 {
						 Number = menuNumber,
						 Name = pizza.Name,
						 Ingredients = pizza.Standard.Select(x => x.Description()).ToList(),
						 Price = pizza.StandardPrice,
					 }
					 );
				menuNumber++;
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
