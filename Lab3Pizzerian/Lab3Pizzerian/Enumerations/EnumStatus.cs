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
		[Description("Placed")]
		Placed = 3,
		[Description("Created")]
		Created = 4,
        Processing = 5
    }
}
