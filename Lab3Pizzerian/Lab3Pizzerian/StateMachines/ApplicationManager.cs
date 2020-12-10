using Lab3Pizzerian.Enumerations;

namespace Lab3Pizzerian
{
    //EditSoda

    //AddPizza
    //EditPizza         skicka in valdpizza i controllen? kanske inte spelar någon roll om den kan göras i open alltid

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
                        case EnumApplicationAction.CompletePayment:
                        case EnumApplicationAction.OpenNewOrder:
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
                        case EnumApplicationAction.CancelCart:
                        case EnumApplicationAction.PlaceOrder:
                            return true;
                        
                        default:
                            return false;
                    }
                default:
                    return false;
            }
        }

        public bool SetState(EnumApplicationAction action)
        {
            if (IsActionAllowed(action))
            {
                switch (action)
                {
                    case EnumApplicationAction.CancelCart:
                    case EnumApplicationAction.PlaceOrder:
                        State = EnumApplicationState.Idle;
                        return true;

                    case EnumApplicationAction.OpenNewOrder:
                        State = EnumApplicationState.CartOpen;
                        return true;

                    default: return false;
                }
            } else
            {
                return false;
            }
        }
    }
}
