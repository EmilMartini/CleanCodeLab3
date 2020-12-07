using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Extensions
{
	public static class EnumExtension
	{
		public static string Description(this object enumerationValue)
		{
			if (enumerationValue == null)
				return string.Empty;

			var type = enumerationValue.GetType();

			//Try to find a DescriptionAttribute for a potential friendly name for the enum
			var memberInfo = type.GetMember(enumerationValue.ToString());
			if (memberInfo != null && memberInfo.Length > 0)
			{
				var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

				if (attributes != null && attributes.Length > 0)
				{
					//Pull out the description value
					return ((DescriptionAttribute)attributes[0]).Description;
				}
			}
			//If we have no description attribute, just return the ToString of the enum
			return enumerationValue.ToString();
		}
	}
}
