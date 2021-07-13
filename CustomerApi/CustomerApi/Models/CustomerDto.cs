using CustomerApi.Models;
using System.Collections.Generic;

namespace CustomerApi
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<AddressDto> Addresses { get; set; } = new List<AddressDto>();
    }
}
