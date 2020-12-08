using Lab3Pizzerian.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Models
{
	public class PizzaDisplayModel
	{
		public string Name { get; set; }
		public List<string> Ingredients { get; set; }
		public List<string> Extras { get; set; }
		public int StandardPrice { get; set; }
		public int ExtrasPrice { get; set; }
	}
}
