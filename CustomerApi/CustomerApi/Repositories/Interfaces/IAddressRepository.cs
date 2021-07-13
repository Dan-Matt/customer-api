using CustomerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<AddressDto>> GetAllAsync();
        Task<int> CreateAsync(AddressDto address, int customerId);
        Task<IEnumerable<AddressDto>> GetByCustomerIdAsync(int id);
        Task DeleteByCustomerIdAsync(int id);
        Task ClearMainForCustomerIdAsync(int customerId);
        Task<AddressDto> GetByIdAsync(int id);
        Task DeleteById(int id);
        Task UpdateAsync(AddressDto address);
        Task<int> GetCustomerIdByAddressIdAsync(int id);
    }
}
