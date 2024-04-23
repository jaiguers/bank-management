using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.EntityFrameworkCore;
using MongoDbGenericRepository.Attributes;

namespace BankAPI.Domain.Models
{
    [CollectionName("roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
    }
}
