using Lab3Pizzerian.Enumerations;
using Lab3Pizzerian.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3Pizzerian
{
	public class Application
	{
		private static Application instance = null;
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
		public ApplicationManager ApplicationManager { get; } = new ApplicationManager();

		public static Application GetApplicationInstance()
		{
			if (instance == null)
			{
				instance = new Application();
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
			if(Cart.Pizzas.Count <= 0 && Cart.Drinks.Count <= 0)
            {
				return null;
            }

			Cart.OrderStatus = EnumStatus.Processing;
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
        public bool CompletePayment(int orderId)
        {
            try
            {
				Orders.Where(i => i.ID == orderId).Select(i => i.OrderStatus = EnumStatus.Placed).ToList();
				return true;
            }
            catch (Exception)
            {
				return false;
            }
        }
        public bool CancelCart()
        {
			Cart = null;
			return (Cart == null);
        }
        public bool RemoveDrink(int index)
        {
			string nameOfDrink = Cart.Drinks.ElementAt(index - 1).ToString();
			int instancesOfDrink = Cart.Drinks.Where(i => i.ToString() == nameOfDrink).Count();
			Cart.Drinks.RemoveAt(index - 1);
			return instancesOfDrink != Cart.Drinks.Where(i => i.ToString() == nameOfDrink).Count();
        }
    }
}
