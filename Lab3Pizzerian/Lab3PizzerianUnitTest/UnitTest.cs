using Lab3Pizzerian;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab3PizzerianUnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CreateOrderTest_StateIdle()
        {
            var instance = MockDb.GetDbInstance();
            var order = new Order()
            {
                ID = 1,
                Pizzas = new System.Collections.Generic.List<Lab3Pizzerian.Models.Pizza>(),
                Drinks = new System.Collections.Generic.List<Lab3Pizzerian.Enumerations.EnumDrink>()
            };
            var expected = true;

            var actual = instance.CreateOrder(order);

            Assert.AreEqual(expected, actual);
        }       
    }
}
