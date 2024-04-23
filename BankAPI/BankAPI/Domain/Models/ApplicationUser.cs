using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.EntityFrameworkCore;
using MongoDbGenericRepository.Attributes;

namespace BankAPI.Domain.Models
{
    [CollectionName("users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public Person Person { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
