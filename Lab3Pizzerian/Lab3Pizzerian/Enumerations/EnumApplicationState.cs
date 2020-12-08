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
		[Description("Application Open")]
		Open = 2,
		[Description("Application Closed")]
		Closed = 3
	}
}
