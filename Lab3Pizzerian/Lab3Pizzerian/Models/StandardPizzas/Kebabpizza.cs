using Lab3Pizzerian.Enumerations;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Models
{
	public class Kebabpizza : StandardPizzaPrototype
	{
		public Kebabpizza()
		{
			Name = "Kebabpizza";
			Standard = new List<EnumIngredient>{
						  EnumIngredient.Cheese,
						  EnumIngredient.Tomatosauce,
						  EnumIngredient.Kebab,
						  EnumIngredient.Mushrooms,
						  EnumIngredient.Onion,
						  EnumIngredient.Pepperoni,
						  EnumIngredient.Salad,
						  EnumIngredient.Tomato,
						  EnumIngredient.KebabSauce };
			StandardPrice = 105;
			Extras = new List<EnumIngredient>();
		}
		public override StandardPizzaPrototype Clone()
		{
			return (StandardPizzaPrototype)this.MemberwiseClone(); // Clones the concrete class.
		}
	};
}
