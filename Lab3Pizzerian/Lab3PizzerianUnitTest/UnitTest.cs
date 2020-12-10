using Lab3Pizzerian;
using Lab3Pizzerian.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab3PizzerianUnitTest
{
    [TestClass]
    public class UnitTest
    {
        ApiController controller = new ApiController();

        [TestMethod]
        public void IsActionAllowedTest()
        {
            var instance = Application.GetApplicationInstance();

            var actual1 = instance.ApplicationManager.IsActionAllowed(Lab3Pizzerian.Enumerations.EnumApplicationAction.PlaceOrder);
            var actual2 = instance.ApplicationManager.IsActionAllowed(Lab3Pizzerian.Enumerations.EnumApplicationAction.OpenNewOrder);
            var actual3 = instance.ApplicationManager.IsActionAllowed(Lab3Pizzerian.Enumerations.EnumApplicationAction.CancelCart);
            var actual4 = instance.ApplicationManager.IsActionAllowed(Lab3Pizzerian.Enumerations.EnumApplicationAction.GetPlacedOrders);

            Assert.AreEqual(false, actual1);
            Assert.AreEqual(true, actual2);
            Assert.AreEqual(false, actual3);
            Assert.AreEqual(true, actual4);
        }

        [TestMethod]
        public void SetStateTest()
        {
            var instance = Application.GetApplicationInstance();

            var actual1 = instance.ApplicationManager.SetState(Lab3Pizzerian.Enumerations.EnumApplicationAction.OpenNewOrder);
            var actual2 = instance.ApplicationManager.SetState(Lab3Pizzerian.Enumerations.EnumApplicationAction.CompleteOrder);
            var actual3 = instance.ApplicationManager.SetState(Lab3Pizzerian.Enumerations.EnumApplicationAction.EditPizza);
            var actual4 = instance.ApplicationManager.SetState(Lab3Pizzerian.Enumerations.EnumApplicationAction.GetPlacedOrders);

            Assert.AreEqual(true, actual1);
            Assert.AreEqual(false, actual2);
            Assert.AreEqual(false, actual3);
            Assert.AreEqual(false, actual4);
        }
    }
}
