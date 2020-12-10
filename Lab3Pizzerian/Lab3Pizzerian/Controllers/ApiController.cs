using Lab3Pizzerian.Enumerations;
using Lab3Pizzerian.Extensions;
using Lab3Pizzerian.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3Pizzerian.Controllers
{
    [Route("Api")]
    public class ApiController : Controller
    {
        [SwaggerOperation(Summary = "Creates a new cart")]
        [Route("Cart")]
        [HttpPost]
        public IActionResult CreateOrder()
        {
            Application instance = Application.GetApplicationInstance();
            if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.OpenNewOrder) == false)
            {
                return new ConflictObjectResult("You cant open a new cart now");
            }
            var order = new Order();
            order.Id = instance.GetNextOrderId();
            var accepted = instance.CreateOrder(order);
            if (accepted)
            {
                instance.ApplicationManager.SetState(EnumApplicationAction.OpenNewOrder);
                return new OkObjectResult($"Cart created with OrderId: {order.Id}{Environment.NewLine}");
            }
            else
            {
                return new ConflictObjectResult($"OrderId: {order.Id} already exists.");
            }
        }

        [SwaggerOperation(Summary = "Removes the current cart")]
        [Route("Cart")]
        [HttpDelete]
        public IActionResult EmptyCart()
        {
            Application instance = Application.GetApplicationInstance();
            if (!instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.CancelCart))
            {
                return new ConflictObjectResult("You cannot empty current cart right now.");
            }

            var success = instance.CancelCart();

            if (success)
            {
                instance.ApplicationManager.SetState(EnumApplicationAction.CancelCart);
                return new OkObjectResult("Successfully emptied current cart");
            }
            else
            {
                return new BadRequestObjectResult("Something went wrong. Couldn't empty current cart");
            }
        }

        [SwaggerOperation(Summary = "Add Pizza to cart")]
        [Route("Cart/Pizza/{MenuNumber}")]
        [HttpPut]
        public IActionResult AddPizza(int MenuNumber)
        {
            Application instance = Application.GetApplicationInstance();
            if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.AddPizza) == false)
            {
                return new ConflictObjectResult("You cant add a pizza now");
            }

            if (instance.Menu.Count() < MenuNumber || 0 >= MenuNumber)
            {
                return new ConflictObjectResult("Thats not on the menu");
            }
            var pizzaToAdd = instance.Menu[MenuNumber - 1].Clone();
            instance.Cart.Pizzas.Add((Pizza)pizzaToAdd);
            return new OkObjectResult($"You have added {pizzaToAdd.Name} to your order");
        }

        [SwaggerOperation(Summary = "Add Drink to cart")]
        [Route("Cart/Drink/{MenuNumber}")]
        [HttpPut]
        public IActionResult AddDrink(int MenuNumber)
        {
            Application instance = Application.GetApplicationInstance();
            if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.AddPizza) == false)
            {
                return new ConflictObjectResult("You cant add a pizza now");
            }

            if (instance.Drinks.Count() < MenuNumber || 0 >= MenuNumber)
            {
                return new ConflictObjectResult("Thats not on the menu");
            }
            var drinkToAdd = instance.Drinks.ElementAt(MenuNumber - 1).Key;
            instance.Cart.Drinks.Add(drinkToAdd);
            return new OkObjectResult($"You have added the {drinkToAdd.ToString()} to your order");
        }

        [SwaggerOperation(Summary = "Remove Drink from cart")]
        [Route("Cart/Drink/{DrinkNumber}")]
        [HttpDelete]
        public IActionResult RemoveDrink(int DrinkNumber)
        {
            Application instance = Application.GetApplicationInstance();
            if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.AddPizza) == false)
            {
                return new ConflictObjectResult("You cant add a pizza now");
            }

            if(instance.Cart.Drinks.Count <= 0)
            {
                return new ConflictObjectResult("Cannot remove any drinks");
            }

            var success = instance.RemoveDrink(DrinkNumber);

            if (success)
            {
                return new OkObjectResult("Drink removed");
            }
            else
            {
                return new BadRequestObjectResult("Cannot remove drink");
            }
        }

        [SwaggerOperation(Summary = "Add Ingredient to pizza")]
        [Route("Cart/{PizzaNumber}/{IngredientNumber}")]
        [HttpPut]
        public IActionResult AddIngreditenToPizza(int PizzaNumber, int IngredientNumber)
        {
            Application instance = Application.GetApplicationInstance();
            if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.EditPizza) == false)
            {
                return new ConflictObjectResult("You cant Edit a pizza now");
            }
            var numberOfAddedPizzas = instance.Cart.Pizzas.Count();
            if (numberOfAddedPizzas == 0 || numberOfAddedPizzas < PizzaNumber)
            {
                return new ConflictObjectResult("You can only add ingreditents to a pizza you have added");
            }
            if (IngredientNumber <= 0 || IngredientNumber > 10)
            {
                return new ConflictObjectResult("You can only add ingreditents that exists");
            }
            var pizzaToAddIngredientTo = instance.Cart.Pizzas[PizzaNumber - 1];
            pizzaToAddIngredientTo.Extras.Add((EnumIngredient)IngredientNumber);
            return new OkObjectResult($"You have added {((EnumIngredient)IngredientNumber).Description()} to your {pizzaToAddIngredientTo.Name}");
        }

        [SwaggerOperation(Summary = "Remove a ingredient you have added to you pizza")]
        [Route("Cart/{PizzaNumber}/{IngredientNumber}")]
        [HttpDelete]
        public IActionResult RemoveAddedIngreditenFromPizza(int PizzaNumber, int IngredientNumber)
        {
            Application instance = Application.GetApplicationInstance();
            if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.EditPizza) == false)
            {
                return new ConflictObjectResult("You cant Edit a pizza now");
            }
            var numberOfAddedPizzas = instance.Cart.Pizzas.Count();
            if (numberOfAddedPizzas == 0 || numberOfAddedPizzas < PizzaNumber)
            {
                return new ConflictObjectResult("You can only remove ingreditents to a pizza you have added");
            }
            if (IngredientNumber <= 0 || IngredientNumber > 15)
            {
                return new ConflictObjectResult("You can only remove ingreditents that exists");
            }
            var pizza = instance.Cart.Pizzas[PizzaNumber - 1];
            if (!pizza.Extras.Contains((EnumIngredient)IngredientNumber))
            {
                return new ConflictObjectResult("You can only remove ingreditents that exists");
            }
            pizza.Extras.Remove((EnumIngredient)IngredientNumber);
            return new OkObjectResult($"You have Removed {((EnumIngredient)IngredientNumber).Description()} to your {pizza.Name}");
        }

        [SwaggerOperation(Summary = "View the current cart")]
        [Route("Cart")]
        [HttpGet]
        public IActionResult ViewCart()
        {
            Application instance = Application.GetApplicationInstance();
            if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.ViewCart) == false)
            {
                return new ConflictObjectResult("You cant view your cart now");
            }
            var viewOrderModel = new OrderDisplayModel()
            {
                Drinks = new List<string>(),
                Pizzas = new List<PizzaDisplayModel>(),
                TotalPrice = instance.GetCartTotalCost(),
                OrderId = instance.Cart.Id.ToString()
            };

            foreach (var item in instance.Cart.Drinks)
            {
                viewOrderModel.Drinks.Add(item.Description());
            }

            foreach (var pizza in instance.Cart.Pizzas)
            {
                List<string> extraIngr = new List<string>();
                foreach (var extraIngredient in pizza.Extras)
                {
                    extraIngr.Add(extraIngredient.Description());
                }
                List<string> standardIngr = new List<string>();
                foreach (var standardIngredient in pizza.Standard)
                {
                    standardIngr.Add(standardIngredient.Description());
                }
                viewOrderModel.Pizzas.Add(new PizzaDisplayModel()
                {
                    Name = pizza.Name,
                    Ingredients = standardIngr,
                    Extras = extraIngr,
                    StandardPrice = pizza.StandardPrice,
                    ExtrasPrice = instance.GetIngredientListPrice(pizza.Extras),
                });
            }
            return new OkObjectResult(viewOrderModel);
        }

        [SwaggerOperation(Summary = "Place Order")]
        [Route("Order")]
        [HttpPost]
        public IActionResult PlaceOrder()
        {
            Application instance = Application.GetApplicationInstance();
            if (!instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.PlaceOrder))
            {
                return new ConflictObjectResult("You cant place your order now");
            }
            var order = instance.PlaceOrder();
            if (order == null)
            {
                return new BadRequestObjectResult("Cannot place an empty order.");
            }


            var orderMenuModel = new OrderDisplayModel()
            {
                OrderId = order.Id.ToString(),
                Drinks = order.Drinks.Select(i => i.Description()).ToList(),
                TotalPrice = instance.GetOrderTotalCost(order)
            };

            foreach (var pizza in order.Pizzas)
            {
                orderMenuModel.Pizzas.Add(new PizzaDisplayModel()
                {
                    Name = pizza.Name,
                    Extras = pizza.Extras.Select(i => i.Description()).ToList(),
                    Ingredients = pizza.Standard.Select(i => i.Description()).ToList(),
                    StandardPrice = pizza.StandardPrice,
                    ExtrasPrice = instance.GetIngredientListPrice(pizza.Extras)
                });
            }

            instance.ApplicationManager.SetState(EnumApplicationAction.PlaceOrder);
            return new OkObjectResult(orderMenuModel);
        }

        [SwaggerOperation(Summary = "Completes an order")]
        [Route("Order/{OrderId:int}")]
        [HttpPost]
        public IActionResult CompleteOrder(int OrderId)
        {
            Application instance = Application.GetApplicationInstance();
            if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.CompleteOrder) == false)
            {
                return new ConflictObjectResult("You cannot complete the order right now.");
            }

            var accepted = instance.CompleteOrder(OrderId);
            if (accepted)
            {
                return new OkObjectResult($"Order Completed.");
            }
            else
            {
                return new BadRequestObjectResult($"Cannot complete order.");
            }
        }

        [SwaggerOperation(Summary = "Cancels an order")]
        [Route("Order/{OrderId:int}")]
        [HttpDelete]
        public IActionResult CancelOrder(int OrderId)
        {
            Application instance = Application.GetApplicationInstance();
            if (instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.CancelOrder) == false)
            {
                return new ConflictObjectResult("You cant Edit a pizza now");
            }

            var accepted = instance.CancelOrder(OrderId);
            if (accepted)
            {
                return new OkObjectResult($"Order Canceled.");
            }
            else
            {
                return new BadRequestObjectResult($"Cannot cancel order.");
            }

        }

        [SwaggerOperation(Summary = "Get placed orders")]
        [Route("Order")]
        [HttpGet]
        public IActionResult GetPlacedOrders()
        {
            Application instance = Application.GetApplicationInstance();
            if (!instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.GetPlacedOrders))
            {
                return new ConflictObjectResult("You cannot view placed orders right now.");
            }
            var orders = instance.GetPlacedOrders();
            var jsonOrders = new List<OrderDisplayModel>();

            foreach (var order in orders)
            {
                var orderDisplayModel = new OrderDisplayModel();
                foreach (var pizza in order.Pizzas)
                {
                    var pizzaModel = new PizzaDisplayModel();
                    pizzaModel.Name = pizza.Name;
                    pizzaModel.Extras = pizza.Extras.Select(i => i.Description()).ToList();
                    pizzaModel.Ingredients = pizza.Standard.Select(i => i.Description()).ToList();
                    pizzaModel.StandardPrice = pizza.StandardPrice;
                    pizzaModel.ExtrasPrice = instance.GetIngredientListPrice(pizza.Extras);
                    orderDisplayModel.Pizzas.Add(pizzaModel);

                }
                orderDisplayModel.Drinks = order.Drinks.Select(i => i.Description()).ToList();
                orderDisplayModel.TotalPrice = instance.GetOrderTotalCost(order);
                orderDisplayModel.OrderId = order.Id.ToString();
                jsonOrders.Add(orderDisplayModel);
            }

            return new OkObjectResult(jsonOrders);
        }

        [SwaggerOperation(Summary = "Get current pizza menu")]
        [Route("Menu/Pizza")]
        [HttpGet]
        public IActionResult GetMenu()
        {
            Application instance = Application.GetApplicationInstance();
            var pizzaMenu = instance.GetMenu();
            var pizzaMenuModel = new List<PizzaDisplayMenuModel>();
            int menuNumber = 1;
            foreach (var pizza in pizzaMenu)
            {
                pizzaMenuModel.Add(
                     new PizzaDisplayMenuModel
                     {
                         Number = menuNumber,
                         Name = pizza.Name,
                         Ingredients = pizza.Standard.Select(x => x.Description()).ToList(),
                         Price = pizza.StandardPrice,
                     }
                     );
                menuNumber++;
            }
            if (pizzaMenuModel != null)
            {
                return new OkObjectResult(pizzaMenuModel);
            }
            else
            {
                return NoContent();
            }
        }

        [SwaggerOperation(Summary = "Get available ingredients")]
        [Route("Menu/Ingredients")]
        [HttpGet]
        public IActionResult GetIngredients()
        {
            Application instance = Application.GetApplicationInstance();
            var ingredients = instance.Ingredients.Where(i => i.Value > 0).Select(i => new { Ingredient = (i.Key.Description()), Price = i.Value }).ToList();
            if (ingredients.Any())
            {
                return new OkObjectResult(ingredients);
            } else
            {
                return new NoContentResult();
            }
            
        }

        [SwaggerOperation(Summary = "Get available drinks")]
        [Route("Menu/Drinks")]
        [HttpGet]
        public IActionResult GetDrinks()
        {
            Application instance = Application.GetApplicationInstance();
            var drink = instance.Drinks.Select(i => new { Ingredient = (i.Key.Description()), Price = i.Value }).ToList();
            if (drink.Any())
            {
                return new OkObjectResult(drink);
            }
            else
            {
                return new NoContentResult();
            }

        }

        [SwaggerOperation(Summary = "Complete payment for an order")]
        [Route("Payment/{OrderId:int}")]
        [HttpPost]
        public IActionResult CompletePayment(int OrderId)
        {
            Application instance = Application.GetApplicationInstance();
            if (!instance.ApplicationManager.IsActionAllowed(EnumApplicationAction.GetPlacedOrders))
            {
                return new ConflictObjectResult("You cannot complete orderpayment right now.");
            }

            var success = instance.CompletePayment(OrderId);
            if (success)
            {
                return new OkObjectResult("Successfully completed payment for order: " + OrderId);
            }
            else
            {
                return new BadRequestObjectResult("Something went wrong. Couldn't complete payment for order: " + OrderId);
            }
        }
    }
}
