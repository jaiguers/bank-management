using BankAPI.CrossCutting.AppModelDtos;

namespace BankAPI.Domain.Models
{
    public class Person
    {
        public string FullName { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string DocNumber { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<Address> AddressList { get; set; }
    }
}
