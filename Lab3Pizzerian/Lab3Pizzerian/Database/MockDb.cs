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
	public class MockDb
	{
		private static MockDb instance = null;
		public List<Order> Orders { get; set; } = new List<Order>();
		public List<Pizza> Menu { get; set; }
		public Dictionary<EnumIngredient, int> Ingredients { get; set; }
		private readonly Dictionary<EnumDrink, int> Drinks = new Dictionary<EnumDrink, int>
		{
			{ EnumDrink.CocaCola, 20},
			{ EnumDrink.Fanta, 20 },
			{ EnumDrink.Sprite, 25 }
		};

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

		public bool CreateOrder(Order order)
        {
            if (Orders.Any(a => a.ID == order.ID))
            {
				return false;
            } else
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

        public List<Pizza> GetMenu()
        {
			return Menu;
        }
    }
}
