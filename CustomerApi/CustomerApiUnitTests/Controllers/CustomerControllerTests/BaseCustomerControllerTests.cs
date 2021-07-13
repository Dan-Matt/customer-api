using CustomerApi.Controllers;
using CustomerApi.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace CustomerApiUnitTests.Controllers.CustomerControllerTests
{
    public class BaseCustomerControllerTests
    {
        protected CustomerController customerController;
        protected Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();

        public BaseCustomerControllerTests()
        {
            customerController = new CustomerController(mockCustomerService.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            mockCustomerService.Invocations.Clear();
        }
    }
}