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
		//ViewOpenOrder = 7,


		// kvar att gära
		//CompleteOrder = 5,
		//CancelOrder = 6,
		//add/remove dricka
		//view ingredients som man kan lägga på
		// bara kunna lägga på saker som kostar


		[SwaggerOperation(Summary = "Creates a new order")]
		[Route("CreateOrder")]
		[HttpPost]
		public IActionResult CreateOrder()
		{
			MockDb instance = MockDb.GetDbInstance();
			if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.OpenNewOrder) == false)
			{
				return new ConflictObjectResult("You cant open a new order now");
			}
			var order = new Order();
			order.ID = Guid.NewGuid();
			var accepted = instance.CreateOrder(order);
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
		[Route("AddPizza/{MenuNumber}")]
		[HttpPut]
		public IActionResult AddPizza(int MenuNumber)
		{
			MockDb instance = MockDb.GetDbInstance();
			if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.AddPizza) == false)
			{
				return new ConflictObjectResult("You cant add a pizza now");
			}
			if (instance.Menu.Count() < MenuNumber || 0 >= MenuNumber)
			{
				return new ConflictObjectResult("Thats not on the menu");
			}
			var pizzaToAdd = instance.Menu[MenuNumber - 1].Clone();
			instance.Order.Pizzas.Add((Pizza)pizzaToAdd);
			return new OkObjectResult($"You have added the pizza {pizzaToAdd.Name} to your order");
		}

		[SwaggerOperation(Summary = "Add Ingredient to pizza")]
		[Route("AddIngreditenToPizza/{PizzaNumber}/{IngredientNumber}")]
		[HttpPut]
		public IActionResult AddIngreditenToPizza(int PizzaNumber, int IngredientNumber)
		{
			MockDb instance = MockDb.GetDbInstance();
			if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.EditPizza) == false)
			{
				return new ConflictObjectResult("You cant Edit a pizza now");
			}
			var numberOfAddedPizzas = instance.Order.Pizzas.Count();
			if (numberOfAddedPizzas == 0 || numberOfAddedPizzas < PizzaNumber)
			{
				return new ConflictObjectResult("You can only add ingreditents to a pizza you have added");
			}
			if (IngredientNumber <= 0 || IngredientNumber > 15)
			{
				return new ConflictObjectResult("You can only add ingreditents that exists");
			}
			var pizzaToAddIngredientTo = instance.Order.Pizzas[PizzaNumber - 1];
			pizzaToAddIngredientTo.Extras.Add((EnumIngredient)IngredientNumber);
			return new OkObjectResult($"You have added {((EnumIngredient)IngredientNumber).Description()} to your {pizzaToAddIngredientTo.Name}");
		}

		[SwaggerOperation(Summary = "Remove a ingredient you have added to you pizza")]
		[Route("RemoveAddedIngreditenFromPizza/{PizzaNumber}/{IngredientNumber}")]
		[HttpPut]
		public IActionResult RemoveAddedIngreditenFromPizza(int PizzaNumber, int IngredientNumber)
		{
			MockDb instance = MockDb.GetDbInstance();
			if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.EditPizza) == false)
			{
				return new ConflictObjectResult("You cant Edit a pizza now");
			}
			var numberOfAddedPizzas = instance.Order.Pizzas.Count();
			if (numberOfAddedPizzas == 0 || numberOfAddedPizzas < PizzaNumber)
			{
				return new ConflictObjectResult("You can only remove ingreditents to a pizza you have added");
			}
			if (IngredientNumber <= 0 || IngredientNumber > 15)
			{
				return new ConflictObjectResult("You can only remove ingreditents that exists");
			}
			var pizza = instance.Order.Pizzas[PizzaNumber - 1];
			if (!pizza.Extras.Contains((EnumIngredient)IngredientNumber))
			{
				return new ConflictObjectResult("You can only remove ingreditents that exists");
			}
			pizza.Extras.Remove((EnumIngredient)IngredientNumber);
			return new OkObjectResult($"You have Removed {((EnumIngredient)IngredientNumber).Description()} to your {pizza.Name}");
		}

		[SwaggerOperation(Summary = "Place Order")]
		[Route("PlaceOrder")]
		[HttpPut]
		public IActionResult PlaceOrder()
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
			var viewOrderModel = new OrderDisplayModel()
			{
				Drinks = new List<string>(),
				Pizzas = new List<PizzaDisplayModel>(),
				TotalPrice = instance.GetOrderTotalCost(),
			};

			foreach (var item in instance.Order.Drinks)
			{
				viewOrderModel.Drinks.Add(item.Description());
			}

			foreach (var pizza in instance.Order.Pizzas)
			{
				List<string> extraIngr = new List<string>();
				foreach (var extraIngredient in pizza.Extras)
				{
					extraIngr.Add(extraIngredient.Description());
				}
				List<string> standardIngr = new List<string>();
				foreach (var standardIngredient in pizza.Standard)
				{
					standardIngr.Add(standardIngredient.Description());
				}
				viewOrderModel.Pizzas.Add(new PizzaDisplayModel()
				{
					Name = pizza.Name,
					Ingredients = standardIngr,
					Extras = extraIngr,
					StandardPrice = pizza.StandardPrice,
					ExtrasPrice = instance.GetIngredientListPrice(pizza.Extras),
				}) ;
			}
			instance.Order.OrderStatus = EnumStatus.Placed;
			return new OkObjectResult(viewOrderModel);
		}

		[SwaggerOperation(Summary = "Get current pizza menu")]
		[Route("GetMenu")]
		[HttpGet]
		public IActionResult GetMenu()
		{
			MockDb instance = MockDb.GetDbInstance();
			var pizzaMenu = instance.GetMenu();
			var pizzaMenuModel = new List<PizzaDisplayMenuModel>();
			int menuNumber = 1;
			foreach (var pizza in pizzaMenu)
			{
				pizzaMenuModel.Add(
					 new PizzaDisplayMenuModel
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
	}
}
