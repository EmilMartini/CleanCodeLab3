using Lab3Pizzerian.Enumerations;
using Lab3Pizzerian.Extensions;
using Lab3Pizzerian.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian
{
	public class Order
	{
		public Guid ID { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public List<Pizza2> Pizzas { get; set; } = new List<Pizza2>();
		[JsonConverter(typeof(StringEnumConverter))]
		public List<EnumDrink> Drinks { get; set; } = new List<EnumDrink>();
		[JsonConverter(typeof(StringEnumConverter))]
		public EnumStatus OrderStatus { get; set; } = EnumStatus.Created;
	}
}
