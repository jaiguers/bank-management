using BankAPI.Domain.Models;
using MongoDB.Bson;

namespace BankAPI.Domain.Repository
{
    public class BankRepo
    {
        private readonly BankDbContext _dbContext;
        public BankRepo(BankDbContext bankDbContext)
        {
            _dbContext = bankDbContext;
        }

        public List<Banks> GetAll()
        {
            return _dbContext.Banks.OrderBy(j => j.Name).ToList();
        }

        public Banks GetBanksById(ObjectId id)
        {
            return _dbContext.Banks.FirstOrDefault(j => j.Id == id);
        }
    }
}
