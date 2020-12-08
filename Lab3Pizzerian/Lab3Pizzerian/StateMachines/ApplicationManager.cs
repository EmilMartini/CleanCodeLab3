using Lab3Pizzerian.Enumerations;
using Lab3Pizzerian.Extensions;
using Lab3Pizzerian.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian
{
	//EditSoda
	
	//AddPizza
	//EditPizza         skicka in valdpizza i controllen? kanske inte spelar någon roll om den kan göras i open alltid

	//PlaceOrder        om något finns i kundvagn

	//CompleteOrder
	//CancelOrder
	//ViewOpenOrders
//att i flera steg lägga till och ta bort produkter i en order för att sedan godkänna den.När
//ordern är lagd så ska det komma tillbaka en lista på ingredienser, alla produkter och totalt
//pris.Ordern kan därefter väljas att markeras som “färdig” eller “avbruten”. Det ska också gå
//att få ut en lista på alla orderar som inte är färdiga eller avbrutna ännu.

	public class ApplicationManager
	{
		private EnumApplicationState State { get; set; } = EnumApplicationState.Idle;

		public bool IsActionAllowed(EnumApplicationAction action)
		{
			//TODO ej klar statemachine
			switch (State)
			{
				case EnumApplicationState.Idle:
					if (action == EnumApplicationAction.OpenNewOrder)
					{
						State = EnumApplicationState.Open;
						return true;
					}
					else
					{
						return false;
					}
				case EnumApplicationState.Open:
					if (action == EnumApplicationAction.AddPizza || action == EnumApplicationAction.EditPizza)
					{
						return true;
					}
					else if (action == EnumApplicationAction.CompleteOrder || action == EnumApplicationAction.PlaceOrder)
					{
						State = EnumApplicationState.Closed;
						return true;
					}
					else
					{
						return false;
					}
				case EnumApplicationState.Closed:
					if (action == EnumApplicationAction.ViewOpenOrder)
					{
						return true;
					}
					else
					{
						return false;
					}
				default:
					return false;
			}
		}
	}
}
