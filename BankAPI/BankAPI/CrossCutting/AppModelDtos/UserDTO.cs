namespace BankAPI.CrossCutting.AppModelDtos
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public PersonDTO Person { get; set; }
    }
}
