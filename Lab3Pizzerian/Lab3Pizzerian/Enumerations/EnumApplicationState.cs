using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Enumerations
{
	public enum EnumApplicationState
	{
		[Description("Application Idle")]
		Idle = 1,
		[Description("Application Cart Open")]
		CartOpen = 2,
		[Description("Application Cart Closed")]
		CartClosed = 3
	}
}
