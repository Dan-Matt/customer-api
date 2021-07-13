using CustomerApi.Repositories.Interfaces;
using CustomerApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository customerRepository;
        private IAddressService addressService;

        public CustomerService(ICustomerRepository customerRepository, IAddressService addressService)
        {
            this.customerRepository = customerRepository;
            this.addressService = addressService;
        }

        public async Task<int> CreateAsync(CustomerDto customer)
        {
            var newCustomerId = await customerRepository.CreateAsync(customer);

            foreach (var address in customer.Addresses)
            {
                await addressService.CreateAsync(address, newCustomerId);
            }

            return newCustomerId;
        }

        public async Task DeleteById(int id)
        {
            await addressService.DeleteByCustomerId(id);
            await customerRepository.DeleteById(id);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customerTask = customerRepository.GetAllAsync();
            return await GetAllWithAddressesAsync(customerTask);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync(bool active)
        {
            var customerTask = customerRepository.GetAllAsync(active);
            return await GetAllWithAddressesAsync(customerTask);
        }

        private async Task<IEnumerable<CustomerDto>> GetAllWithAddressesAsync(
            Task<IEnumerable<CustomerDto>> customersTask)
        {
            var addressesTask = addressService.GetAllAsync();

            await Task.WhenAll(customersTask, addressesTask);

            var customers = customersTask.Result;
            var addresses = addressesTask.Result;

            foreach (var customer in customers)
            {
                customer.Addresses = addresses.Where(a => a.CustomerId == customer.Id).ToList();
            }
            
            return customers;
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var customerTask = customerRepository.GetById(id);
            var addressesTask = addressService.GetByCustomerId(id);

            await Task.WhenAll(customerTask, addressesTask);

            var customer = customerTask.Result;
            var addresses = addressesTask.Result;
            
            customer.Addresses = addresses.Where(a => a.CustomerId == customer.Id).ToList();
            return customer;
        }

        public async Task UpdateAsync(CustomerDto customer)
        {
            await customerRepository.UpdateAsync(customer);
        }
    }
}
