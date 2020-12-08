using Lab3Pizzerian.Enumerations;
using Lab3Pizzerian.Models;
using System;
using System.Collections.Generic;

namespace Lab3Pizzerian
{
	public class Order
	{
		public Guid ID { get; set; }
		public List<Pizza> Pizzas { get; set; } = new List<Pizza>();
		public List<EnumDrink> Drinks { get; set; } = new List<EnumDrink>();
		public EnumStatus OrderStatus { get; set; } = EnumStatus.Created;
	}
}
