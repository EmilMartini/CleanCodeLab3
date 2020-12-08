using Lab3Pizzerian.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Models
{
	public class OrderDisplayModel
	{
		public string OrderId { get; set; }
		public List<PizzaDisplayModel> Pizzas { get; set; } = new List<PizzaDisplayModel>();
		public List<string> Drinks { get; set; } = new List<string>();
		public int TotalPrice { get; set; }
	}
}
