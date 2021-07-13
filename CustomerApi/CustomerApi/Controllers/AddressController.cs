using CustomerApi.Models;
using CustomerApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private IAddressService addressService;

        public AddressController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AddressDto address)
        {
            try
            {
                await addressService.CreateAsync(address, address.CustomerId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IEnumerable<AddressDto>> GetAllAsync()
        {
            return await addressService.GetAllAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AddressDto>> GetByIdAsync(int id)
        {
            try
            {
                return await addressService.GetByIdAsync(id);
            }
            catch(InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPatch]
        public async Task Update(AddressDto address)
        {
            await addressService.UpdateAsync(address);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await addressService.DeleteById(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
