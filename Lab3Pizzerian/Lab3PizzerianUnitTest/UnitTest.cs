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
        public void CreateOrderTest_StateIdle()
        {
            var expected = typeof(OkObjectResult);
            var actual = controller.CreateOrder();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateOrderTest_StateCartOpen()
        {
            var expected = typeof(ConflictObjectResult);
            var actual = controller.CreateOrder();

            Assert.AreEqual(expected, actual);
        }
    }
}
