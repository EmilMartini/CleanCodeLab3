using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace Lab3Pizzerian.Enumerations
{
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
