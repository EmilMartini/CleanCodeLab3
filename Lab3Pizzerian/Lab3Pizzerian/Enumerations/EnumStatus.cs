using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Enumerations
{
	public enum EnumStatus
	{
		[Description("Done")]
		Done = 1,
		[Description("Canceled")]
		Canceled = 2,
		[Description("Active")]
		Active = 3
	}
}
