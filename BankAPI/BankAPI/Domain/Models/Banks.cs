using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace BankAPI.Domain.Models
{
    [Collection("banks")]
    public class Banks
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string Country { get; set; }
    }
}
