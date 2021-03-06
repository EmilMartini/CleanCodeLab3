﻿using Lab3Pizzerian.Enumerations;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Models
{
	public class Pizza
	{
		public string Name { get; set; }
		public List<EnumIngredient> Standard { get; set; }
		public int StandardPrice { get; set; }
		public List<EnumIngredient> Extras { get; set; }
	}
}
