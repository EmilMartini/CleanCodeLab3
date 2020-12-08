using Lab3Pizzerian.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Models
{
	public class OrderDisplayModel
	{
		public List<PizzaDisplayModel> Pizzas { get; set; }
		public List<string> Drinks { get; set; }
		public int TotalPrice { get; set; }
	}
}
