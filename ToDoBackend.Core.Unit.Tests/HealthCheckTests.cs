using NUnit.Framework;
using ToDoBackend.Core.Controllers;

namespace Tests
{
    public class HealthCheckTests
    {
        [Test]
        public void HealthCheckControllerReturnsOK()
        {
            var controller = new HealthCheckController();
            var response = controller.Get();

            Assert.That(response, Is.EqualTo("OK"));
        }
    }
}