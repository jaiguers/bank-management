using BankAPI.Domain.Models;
using MongoDB.Bson;

namespace BankAPI.Domain.Business.Interface
{
    public interface IBankService
    {
        List<Banks> GetAllBanks();
        Banks GetBanksById(ObjectId id);
    }
}
