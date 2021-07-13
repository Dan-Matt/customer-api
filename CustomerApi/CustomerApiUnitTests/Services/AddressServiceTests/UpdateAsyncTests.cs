using AutoFixture;
using CustomerApi.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApiUnitTests.Services.AddressServiceTests
{
    public class UpdateAsyncTests : BaseAddressServiceTests
    {
        Fixture fixture = new Fixture();

        [Test]
        public async Task Should_Throw_Error_If_Address_Doesnt_Exist()
        {
            // Arrange
            var address = fixture.Create<AddressDto>();
            address.Main = true;

            mockAddressRepository
                .Setup(s => s.GetByCustomerIdAsync(address.CustomerId))
                .ReturnsAsync(new List<AddressDto>());

            // Act, Assert
            Assert.ThrowsAsync(Is.TypeOf<InvalidOperationException>(),
                async delegate { await addressService.UpdateAsync(address); }
            );
        }

        [Test]
        public async Task Should_Throw_Error_If_Attempting_To_Update_Main_Address_To_Other()
        {
            // Arrange
            var existingAddress = new AddressDto { Id = 1, Main = true };
            var givenAddress = new AddressDto { Id = 1, Main = false };

            mockAddressRepository
                .Setup(s => s.GetByCustomerIdAsync(givenAddress.CustomerId))
                .ReturnsAsync(new List<AddressDto> { existingAddress });

            // Act, Assert
            Assert.ThrowsAsync(Is.TypeOf<ArgumentException>(),
                async delegate { await addressService.UpdateAsync(givenAddress); }
            );
        }

        [Test]
        public async Task Should_Clear_Existing_Main_Address_If_Given_Address_Is_Main_And_Different_To_Existing_Main()
        {
            // Arrange
            var existingAddresses = new List<AddressDto> {
                new AddressDto { Id = 1, Main = true },
                new AddressDto { Id = 2, Main = false }
            };

            var givenAddress = new AddressDto { Id = 2, Main = true, CustomerId = 99 };

            mockAddressRepository
                .Setup(s => s.GetByCustomerIdAsync(givenAddress.CustomerId))
                .ReturnsAsync(existingAddresses);

            // Act
            await addressService.UpdateAsync(givenAddress);

            // Assert
            mockAddressRepository
                .Verify(v => v.ClearMainForCustomerIdAsync(givenAddress.CustomerId), Times.Once);

            mockAddressRepository
                .Verify(v => v.UpdateAsync(givenAddress), Times.Once);
        }

        [Test]
        public async Task Should_Not_Clear_Existing_Main_Address_If_Given_Address_Is_Not_Main()
        {
            // Arrange
            var existingAddresses = new List<AddressDto> {
                new AddressDto { Id = 1, Main = true },
                new AddressDto { Id = 2, Main = false }
            };

            var givenAddress = new AddressDto { Id = 2, Main = false, CustomerId = 99 };

            mockAddressRepository
                .Setup(s => s.GetByCustomerIdAsync(givenAddress.CustomerId))
                .ReturnsAsync(existingAddresses);

            // Act
            await addressService.UpdateAsync(givenAddress);

            // Assert
            mockAddressRepository
                .Verify(v => v.ClearMainForCustomerIdAsync(givenAddress.CustomerId), Times.Never);

            mockAddressRepository
                .Verify(v => v.UpdateAsync(givenAddress), Times.Once);
        }
    }
}
