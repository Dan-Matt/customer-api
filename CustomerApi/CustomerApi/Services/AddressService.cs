using CustomerApi.Models;
using CustomerApi.Repositories.Interfaces;
using CustomerApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Services
{
    public class AddressService : IAddressService
    {
        private IAddressRepository addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public async Task<int> CreateAsync(AddressDto address, int customerId)
        {
            if(address.Main)
            {
                await addressRepository.ClearMainForCustomerIdAsync(customerId);
            }

            return await addressRepository.CreateAsync(address, customerId);
        }

        public async Task DeleteByCustomerId(int id)
        {
            await addressRepository.DeleteByCustomerIdAsync(id);
        }

        public async Task DeleteById(int id)
        {
            var customerId = await addressRepository.GetCustomerIdByAddressIdAsync(id);
            var existingAddresses = await addressRepository.GetByCustomerIdAsync(customerId);
            if (existingAddresses.Count() == 1)
            {
                // Cannot delete last address
                throw new ArgumentException();
            }

            await addressRepository.DeleteById(id);
        }

        public async Task<IEnumerable<AddressDto>> GetAllAsync()
        {
            return await addressRepository.GetAllAsync();
        }

        public async Task<IEnumerable<AddressDto>> GetByCustomerId(int id)
        {
            return await addressRepository.GetByCustomerIdAsync(id);
        }

        public async Task<AddressDto> GetByIdAsync(int id)
        {
            return await addressRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(AddressDto address)
        {
            var existingAddresses = await addressRepository.GetByCustomerIdAsync(address.CustomerId);
            var addressToUpdate = existingAddresses.First(a => a.Id == address.Id);

            if(!address.Main && addressToUpdate.Main)
            {
                // Cannot clear main address
                throw new ArgumentException();
            }
            if (address.Main)
            {
                await addressRepository.ClearMainForCustomerIdAsync(address.CustomerId);
            }

            await addressRepository.UpdateAsync(address);
        }
    }
}
