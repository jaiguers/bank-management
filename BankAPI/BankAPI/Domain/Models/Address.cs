namespace BankAPI.Domain.Models
{
    public class Address
    {
        public string AddressInfo { get; set; } = string.Empty;
        public string Floor { get; set; } = string.Empty;
        public string Apartment { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
    }
}
