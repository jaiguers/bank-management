namespace BankAPI.CrossCutting.AppModelDtos
{
    public class CreateCardsDTO
    {
        public required string CardType { get; set; }
        public required string AccountType { get; set; }
        public required string NameOnCard { get; set; }
        public required string Provider { get; set; }
        public required Guid UserId { get; set; }
    }
}
