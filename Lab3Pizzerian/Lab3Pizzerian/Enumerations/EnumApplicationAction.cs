using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Enumerations
{
	public enum EnumApplicationAction
	{
		OpenNewOrder = 1,
		AddPizza = 2,
		EditPizza = 3,
		PlaceOrder = 4,
		CompleteOrder = 5,
		CancelOrder = 6,
		ViewCart = 7,
		CompletePayment = 8,
        GetPlacedOrders = 9,
		CancelCart = 10,
    }
}
