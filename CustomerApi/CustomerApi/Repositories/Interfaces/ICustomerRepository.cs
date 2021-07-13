using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<IEnumerable<CustomerDto>> GetAllAsync(bool active);
        Task<int> CreateAsync(CustomerDto customer);
        Task<CustomerDto> GetById(int id);
        Task DeleteById(int id);
        Task UpdateAsync(CustomerDto customer);
    }
}
