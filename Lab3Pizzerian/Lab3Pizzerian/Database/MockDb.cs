using Lab3Pizzerian.Enumerations;
using Lab3Pizzerian.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3Pizzerian
{
	public class MockDb
	{
		private static MockDb instance = null;
		public Order Cart { get; set; } = null;
		private List<Order> Orders { get; set; } = new List<Order>();
		public readonly List<StandardPizzaPrototype> Menu = new List<StandardPizzaPrototype>
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
		public ApplicationManager ApplicationManager { get; } = new ApplicationManager(); // inte säker på att detta är rätt

		public static MockDb GetDbInstance()
		{
			if (instance == null)
			{
				instance = new MockDb();
			}
			return instance;
		}

		public int GetDrinkPrice(EnumDrink drink)
		{
			return Drinks.Where(x => x.Key == drink).FirstOrDefault().Value;
		}

		public int GetIngredientPrice(EnumIngredient ingredient)
		{
			return Ingredients.Where(x => x.Key == ingredient).FirstOrDefault().Value;
		}

		public int GetIngredientListPrice(List<EnumIngredient> ingredients)
		{
			int totalPrice = 0;
			foreach (var ingredient in ingredients)
			{
				totalPrice += Ingredients.Where(x => x.Key == ingredient).FirstOrDefault().Value;
			}
			return totalPrice;
		}

		public int GetCartTotalCost()
		{
			int totalCost = 0;

			foreach (var drink in Cart.Drinks)
			{
				totalCost += GetDrinkPrice(drink);
			}
			foreach (var pizza in Cart.Pizzas)
			{
				totalCost += pizza.StandardPrice;
				foreach (var extraIngredient in pizza.Extras)
				{
					totalCost += GetIngredientPrice(extraIngredient);
				}
			}

			return totalCost;
		}

		public int GetOrderTotalCost(Order order)
        {
			int totalCost = 0;
			foreach (var drink in order.Drinks)
			{
				totalCost += GetDrinkPrice(drink);
			}
			foreach (var pizza in order.Pizzas)
			{
				totalCost += pizza.StandardPrice;
				foreach (var extraIngredient in pizza.Extras)
				{
					totalCost += GetIngredientPrice(extraIngredient);
				}
			}
			return totalCost;
		}

		public bool CreateOrder(Order order)
		{
			if (Cart != null)
			{
				return false;
			}
			else
			{
				Cart = order;
				return true;
			}
		}

		public bool CompleteOrder(int OrderId)
        {
			Orders.Where(i => i.ID == OrderId).Select(i => i.OrderStatus = EnumStatus.Done).ToList();
			return true;
        }

		public List<Order> GetPlacedOrders()
        {
			return Orders.Where(i => i.OrderStatus == EnumStatus.Placed).ToList();
        }

		public int GetNextOrderId()
        {
			return Orders.Count() + 1;
        }
        public Order PlaceOrder()
        {
			Cart.OrderStatus = EnumStatus.Placed;
			Orders.Add(Cart);
			var tempCart = Cart;
			Cart = null;
			return tempCart;
        }

		public List<StandardPizzaPrototype> GetMenu()
		{
			return Menu;
		}

        public bool CancelOrder(int OrderId)
        {
			Orders.Where(i => i.ID == OrderId).Select(i => i.OrderStatus = EnumStatus.Canceled).ToList();
			return true;
		}
    }
}
