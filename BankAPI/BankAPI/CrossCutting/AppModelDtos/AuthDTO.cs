namespace BankAPI.CrossCutting.AppModelDtos
{
    public class AuthDTO
    {
        public string? Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? FullName { get; set; }
        public string? Token { get; set; }
    }
}
