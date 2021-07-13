using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<IEnumerable<CustomerDto>> GetAllAsync(bool active);
        Task<int> CreateAsync(CustomerDto customer);
        Task<CustomerDto> GetByIdAsync(int id);
        Task DeleteById(int id);
        Task UpdateAsync(CustomerDto customer);
    }
}
