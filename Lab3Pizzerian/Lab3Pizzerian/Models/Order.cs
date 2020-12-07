using Lab3Pizzerian.Enumerations;
using Lab3Pizzerian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian
{
	public class Order
	{
		public Guid ID { get; set; }
		public List<Pizza> Pizzas { get; set; }
		public List<EnumDrink> Drinks { get; set; }
		public Dictionary<EnumDrink, int> DrinksX { get; set; } // ?? ska nog inte vara här
		// kanske en klass som heter typ Priser? där vi har några Dictionarys med enums och priser
	}
}
