﻿using Lab3Pizzerian.Enumerations;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Models
{
	public class QuatroStagioni : StandardPizzaPrototype
	{
		public QuatroStagioni()
		{
			Name = "Quatro Stagioni";
			Standard = new List<EnumIngredient>{
						  EnumIngredient.Cheese,
						  EnumIngredient.Tomatosauce,
						  EnumIngredient.Ham,
						  EnumIngredient.Shrimps,
						  EnumIngredient.Clams,
						  EnumIngredient.Mushrooms,
						  EnumIngredient.Artichoke };
			StandardPrice = 115;
			Extras = new List<EnumIngredient>();
		}
		public override StandardPizzaPrototype Clone()
		{
			return (StandardPizzaPrototype)this.MemberwiseClone(); // Clones the concrete class.
		}
	};
}
