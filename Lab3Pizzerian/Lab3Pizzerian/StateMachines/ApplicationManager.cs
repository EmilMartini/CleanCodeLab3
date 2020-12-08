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
            switch (State)
            {
                case EnumApplicationState.Idle:
                    switch (action)
                    {
                        case EnumApplicationAction.GetPlacedOrders:
                        case EnumApplicationAction.CompleteOrder:
                        case EnumApplicationAction.CancelOrder:
                            return true;
                        case EnumApplicationAction.OpenNewOrder:
                            State = EnumApplicationState.CartOpen;
                            return true;
                        default:
                            return false;
                    }

                case EnumApplicationState.CartOpen:
                    switch (action)
                    {
                        case EnumApplicationAction.AddPizza:
                        case EnumApplicationAction.EditPizza:
                        case EnumApplicationAction.ViewCart:
                            return true;
                        case EnumApplicationAction.PlaceOrder:
                            State = EnumApplicationState.Idle;
                            return true;
                        default:
                            return false;
                    }
                default:
                    return false;
            }
        }
    }
}
