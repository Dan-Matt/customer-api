using CustomerApi.Repositories.Interfaces;
using CustomerApi.Services;
using CustomerApi.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace CustomerApiUnitTests.Services.AddressServiceTests
{
    public class BaseAddressServiceTests
    {
        protected IAddressService addressService;
        protected Mock<IAddressRepository> mockAddressRepository = new Mock<IAddressRepository>();

        public BaseAddressServiceTests()
        {
            addressService = new AddressService(mockAddressRepository.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            mockAddressRepository.Invocations.Clear();
        }
    }
}
