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
		public Order Order { get; set; } = null; // kundvagn
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

		public int GetOrderTotalCost()
		{
			int totalCost = 0;

			foreach (var drink in Order.Drinks)
			{
				totalCost += GetDrinkPrice(drink);
			}
			foreach (var pizza in Order.Pizzas)
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

		public List<StandardPizzaPrototype> GetMenu()
		{
			return Menu;
		}
	}
}
