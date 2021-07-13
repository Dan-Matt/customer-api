namespace CustomerApi.Models
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public bool Main { get; set; }
        public int CustomerId { get; set; }
    }
}
