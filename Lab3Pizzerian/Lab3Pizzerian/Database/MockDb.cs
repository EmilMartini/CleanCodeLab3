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
