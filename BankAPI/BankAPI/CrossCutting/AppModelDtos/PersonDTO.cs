namespace BankAPI.CrossCutting.AppModelDtos
{
    public class PersonDTO
    {
        public string FullName { get; set; }
        public string DocumentType { get; set; }
        public string DocNumber { get; set; }
        public string Phone { get; set; }
        public List<AddressDTO> AddressList { get; set; }
    }
}
