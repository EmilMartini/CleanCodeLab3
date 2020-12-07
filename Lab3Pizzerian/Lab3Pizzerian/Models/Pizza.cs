using Lab3Pizzerian.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Models
{
	public class Pizza
	{
		public string Name { get; set; }
		public List<EnumIngredient> Standard { get; set; }
		public List<EnumIngredient> Extras { get; set; }
		// kan vi använda PrototypeDesignpattern när vi skapar pizzor? 
		// Där vår PizzaMeny är mallen för hur pizzan skall se ut i grunden
	}
}
