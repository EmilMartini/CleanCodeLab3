using Lab3Pizzerian.Enumerations;
using Lab3Pizzerian.Extensions;
using Lab3Pizzerian.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian
{
	public enum EnumApplicationAction
	{
		OpenNewOrder = 1,
		AddPizza = 2,
		EditPizza = 3, 
		PlaceOrder = 4,
		CompleteOrder = 5,
		CancelOrder = 6,
		ViewOpenOrder = 7,
	}
	//EditSoda
	
	//AddPizza
	//EditPizza         skicka in valdpizza i controllen? kanske inte spelar någon roll om den kan göras i open alltid

	//PlaceOrder        om något finns i kundvagn

	//CompleteOrder
	//CancelOrder
	//ViewOpenOrders
//att i flera steg lägga till och ta bort produkter i en order för att sedan godkänna den.När
//ordern är lagd så ska det komma tillbaka en lista på ingredienser, alla produkter och totalt
//pris.Ordern kan därefter väljas att markeras som “färdig” eller “avbruten”. Det ska också gå
//att få ut en lista på alla orderar som inte är färdiga eller avbrutna ännu.

	public class ApplicationManager
	{
		private EnumApplicationState State { get; set; } = EnumApplicationState.Idle;

		public EnumApplicationState GetCurrentState()
		{
			return State;
		}
		public bool IsActionAllowed(EnumApplicationAction action)
		{
			switch (State)
			{
				case EnumApplicationState.Idle:
					if (action == EnumApplicationAction.OpenNewOrder)
					{
						State = EnumApplicationState.Open;
						return true;
					}
					else
					{
						return false;
					}
				case EnumApplicationState.Open:
					if (action == EnumApplicationAction.AddPizza || action == EnumApplicationAction.EditPizza)
					{
						return true;
					}
					else if (action == EnumApplicationAction.CompleteOrder || action == EnumApplicationAction.PlaceOrder)
					{
						State = EnumApplicationState.Closed;
						return true;
					}
					else
					{
						return false;
					}
				case EnumApplicationState.Closed:
					if (action == EnumApplicationAction.ViewOpenOrder)
					{
						return true;
					}
					else
					{
						return false;
					}
				default:
					return false;
			}
		}
	}
	public class MockDb
	{
		private static MockDb instance = null;
		public Order Order { get; set; } = null;
		public List<Order> Orders { get; set; } = new List<Order>();
		public readonly List<Pizza> Menu = new List<Pizza>
		  {
				{
					 new Pizza{
					 Name = "Margarita",
					 Standard = new List<EnumIngredient>{
						  EnumIngredient.Ost,
						  EnumIngredient.Tomatsås},
					 Extras = new List<EnumIngredient>(),
					 StandardPrice = 85
					 }
				},
				{
					 new Pizza{
					 Name = "Hawaii",
					 Standard = new List<EnumIngredient>{
						  EnumIngredient.Ost,
						  EnumIngredient.Tomatsås,
						  EnumIngredient.Skinka,
						  EnumIngredient.Ananas},
					 Extras = new List<EnumIngredient>(),
					 StandardPrice = 95
					 }
				},
				{
					 new Pizza{
					 Name = "Kebabpizza",
					 Standard = new List<EnumIngredient>{
						  EnumIngredient.Ost,
						  EnumIngredient.Tomatsås,
						  EnumIngredient.Kebab,
						  EnumIngredient.Champinjoner,
						  EnumIngredient.Lök,
						  EnumIngredient.Feferoni,
						  EnumIngredient.Isbergssallad,
						  EnumIngredient.Tomat,
						  EnumIngredient.Kebabsås},
					 Extras = new List<EnumIngredient>(),
					 StandardPrice = 105
					 }
				},
				{
					 new Pizza{
					 Name = "Quatro Stagioni",
					 Standard = new List<EnumIngredient>{
						  EnumIngredient.Ost,
						  EnumIngredient.Tomatsås,
						  EnumIngredient.Skinka,
						  EnumIngredient.Räkor,
						  EnumIngredient.Musslor,
						  EnumIngredient.Champinjoner,
						  EnumIngredient.Kronärtskocka},
					 Extras = new List<EnumIngredient>(),
					 StandardPrice = 115
					 }
				},
		  };
		public readonly List<StandardPizzaPrototype> Menu2 = new List<StandardPizzaPrototype>
		  {
				{
					 new Margarita()
				},
				{
					 new Hawaii()
				},
				{
					 new Kebabpizza()
				},
				{
					 new QuatroStagioni()
				},
		  };
		public readonly Dictionary<EnumIngredient, int> Ingredients = new Dictionary<EnumIngredient, int>
		  {
				{ EnumIngredient.Ost, 0},
				{ EnumIngredient.Tomatsås, 0},
				{ EnumIngredient.Feferoni, 0},
				{ EnumIngredient.Tomat, 0},
				{ EnumIngredient.Isbergssallad, 0},
				{ EnumIngredient.Skinka, 10},
				{ EnumIngredient.Ananas, 10},
				{ EnumIngredient.Champinjoner, 10},
				{ EnumIngredient.Lök, 10},
				{ EnumIngredient.Kebabsås, 10},
				{ EnumIngredient.Räkor, 15},
				{ EnumIngredient.Musslor, 15},
				{ EnumIngredient.Kronärtskocka, 15},
				{ EnumIngredient.Kebab, 20},
				{ EnumIngredient.Koriander, 20},
		  };
		public readonly Dictionary<EnumDrink, int> Drinks = new Dictionary<EnumDrink, int>
		  {
				{ EnumDrink.CocaCola, 20},
				{ EnumDrink.Fanta, 20 },
				{ EnumDrink.Sprite, 25 }
		  };
		public ApplicationManager ApplicationManager { get; } = new ApplicationManager();

		public static MockDb GetDbInstance()
		{
			if (instance == null)
			{
				instance = new MockDb();
			}
			return instance;
		}
		// eventuellt göra en listsök
		public int GetDrinkPrice(EnumDrink drink)
		{
			return Drinks.Where(x => x.Key == drink).FirstOrDefault().Value;
		}

		//samma för ingredients GetIngredientPrice
		public bool CreateOrder2(Order order)
		{
			if (Order != null)
			{
				return false;
			}
			else
			{
				Order = order;
				return true;
			}
		}
		public bool OpenOrderExist2()
		{
			if (Order != null)
			{
				return true;
			}
			return false;
		}
		public bool CreateOrder(Order order)
		{
			if (Orders.Any(a => a.ID == order.ID))
			{
				return false;
			}
			else
			{
				Orders.Add(order);
				return true;
			}
		}

		public List<Order> GetOrders()
		{
			return Orders;
		}

		public Order GetOrder(string guid)
		{
			var order = Orders.Where(i => i.ID.ToString() == guid).FirstOrDefault();
			return order;
		}

		public List<Order> GetOrders(EnumStatus OrderStatus)
		{
			var orders = Orders.Where(i => i.OrderStatus == OrderStatus).ToList();
			return orders;
		}

		public List<Pizza> GetMenu()
		{
			return Menu;
		}

		public List<StandardPizzaPrototype> GetMenuPrototypeTest()
		{
			return Menu2;
		}
	}
}
