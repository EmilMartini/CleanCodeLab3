using Lab3Pizzerian.Enumerations;

namespace Lab3Pizzerian
{
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

                    default: 
                        return false;
                }
            }
            
            return false;
        }
    }
}
