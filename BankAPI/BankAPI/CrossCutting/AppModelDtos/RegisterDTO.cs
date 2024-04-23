namespace BankAPI.CrossCutting.AppModelDtos
{
    public class RegisterDTO:AuthDTO
    {
        public PersonDTO Person { get; set; }
    }
}
