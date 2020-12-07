using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Enumerations
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnumStatus
	{
		[Description("Done")]
		Done = 1,
		[Description("Canceled")]
		Canceled = 2,
		[Description("Active")]
		Active = 3,
		[Description("Created")]
		Created = 4
	}
}
