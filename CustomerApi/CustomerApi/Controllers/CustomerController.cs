using CustomerApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CustomerDto customer)
        {
            if (!customer.Addresses.Any())
            {
                return BadRequest();
            }

            try
            {
                await customerService.CreateAsync(customer);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CustomerDto>> GetByIdAsync(int id)
        {
            try
            {
                return await customerService.GetByIdAsync(id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerDto>> GetAllActiveAsync([FromQuery] bool? active = null)
        {
            return active.HasValue 
                ? await customerService.GetAllAsync(active.Value) 
                : await customerService.GetAllAsync();
        }

        [HttpPatch]
        public async Task Update(CustomerDto customer)
        {
            await customerService.UpdateAsync(customer);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(int id)
        {
            await customerService.DeleteById(id);
        }

    }
}
