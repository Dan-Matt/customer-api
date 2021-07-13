using CustomerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Services.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAllAsync();
        Task<int> CreateAsync(AddressDto address, int customerId);
        Task<IEnumerable<AddressDto>> GetByCustomerId(int id);
        Task DeleteByCustomerId(int id);
        Task<AddressDto> GetByIdAsync(int id);
        Task DeleteById(int id);
        Task UpdateAsync(AddressDto address);
    }
}
