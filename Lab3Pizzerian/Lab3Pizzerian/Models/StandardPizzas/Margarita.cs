using Lab3Pizzerian.Enumerations;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Models
{

	public class Margarita : StandardPizzaPrototype
	{
		public Margarita()
		{
			Name = "Margarita";
			Standard = new List<EnumIngredient>{
						  EnumIngredient.Ost,
						  EnumIngredient.Tomatsås};
			StandardPrice = 85;
			Extras = new List<EnumIngredient>();
		}
		public override StandardPizzaPrototype Clone()
		{
			return (StandardPizzaPrototype)this.MemberwiseClone(); // Clones the concrete class.
		}
	}
}
