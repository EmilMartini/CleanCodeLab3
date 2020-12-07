using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Enumerations
{
	public enum EnumIngredient
	{
		[Description("Ost")]
		Ost = 1,
		[Description("Tomatsås")]
		Tomatsås = 2,
		[Description("Skinka")]
		Skinka = 3,
		[Description("Ananas")]
		Ananas = 4,
		[Description("Champinjoner")]
		Champinjoner = 5,
		[Description("Lök")]
		Lök = 6,
		[Description("Kebabsås")]
		Kebabsås = 7,
		[Description("Räkor")]
		Räkor = 8,
		[Description("Musslor")]
		Musslor = 9,
		[Description("Kronärtskocka")]
		Kronärtskocka = 10,
		[Description("Kebab")]
		Kebab = 11,
		[Description("Koriander")]
		Koriander = 12
	}
}
