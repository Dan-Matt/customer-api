using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Models
{
    public class CustomerAddressDto
    {
        public int CustomerId { get; set; }
        public int AddressId { get; set; }
    }
}
