namespace BankAPI.CrossCutting.AppModelDtos
{
    public class BankDTO
    {
        public string? Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Status { get; set; }
        public required string Country { get; set; }
    }
}
