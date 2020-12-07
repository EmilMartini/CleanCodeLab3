using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Enumerations
{
	public enum EnumDrink
	{
		[Description("Coca Cola")]
		CocaCola = 1,
		[Description("Fanta")]
		Fanta = 2,
		[Description("Sprite")]
		Sprite = 3,
	}
}
