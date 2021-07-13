using CustomerApi;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using System;
using Moq;
using AutoFixture;

namespace CustomerApiUnitTests.Controllers.CustomerControllerTests
{
    public class CreateAsyncTests : BaseCustomerControllerTests
    {
        Fixture fixture = new Fixture();

        [Test]
        public async Task Should_Return_Bad_Request_When_No_Addresses()
        {
            // Arrange
            var customer = new CustomerDto();

            // Act
            var result = await customerController.CreateAsync(customer);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task Should_Return_Bad_Request_On_Exception()
        {
            // Arrange
            var customer = fixture.Create<CustomerDto>();

            mockCustomerService
                .Setup(s => s.CreateAsync(customer))
                .Throws(new ArgumentException());

            // Act
            var result = await customerController.CreateAsync(customer);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);

            mockCustomerService
                .Verify(v => v.CreateAsync(customer), Times.Once);
        }

        [Test]
        public async Task Should_Return_Ok()
        {
            // Arrange
            var customer = fixture.Create<CustomerDto>();

            // Act
            var result = await customerController.CreateAsync(customer);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);

            mockCustomerService
                .Verify(v => v.CreateAsync(customer), Times.Once);
        }
    }
}