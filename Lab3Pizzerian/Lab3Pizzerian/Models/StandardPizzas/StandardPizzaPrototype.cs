using Lab3Pizzerian.Enumerations;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Models
{
	public abstract class StandardPizzaPrototype : Pizza
	{
		public abstract StandardPizzaPrototype Clone();
	}
}
