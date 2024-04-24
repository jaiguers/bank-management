using BankAPI.Domain.Models;
using MongoDB.Bson;

namespace BankAPI.CrossCutting.AppModelDtos
{
    public class CardsDTO
    {
        public string Id { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string AccountType { get; set; }
        public string NameOnCard { get; set; }
        public string Provider { get; set; }
        public string CVV { get; set; }
        public string ExpirationDate { get; set; }
        public string Balance { get; set; }
        public string Status { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ActivatedAt { get; set; }
        public List<AmountDTO> Amounts { get; set; }
    }
}
