using Lab3Pizzerian.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Models
{
	public class PizzaMenuModel
	{
		public string Name { get; set; }
		public List<string> Ingredents { get; set; }
		public int Price { get; set; }
	}
}
