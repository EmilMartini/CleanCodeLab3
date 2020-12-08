using Lab3Pizzerian.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Models
{
	public class ViewOrderModel
	{
		public List<PizzaModel> Pizzas { get; set; }
		public List<string> Drinks { get; set; }
		public int Price { get; set; }
	}
}
