using BankAPI.Domain.Business.Interface;
using BankAPI.Domain.Models;
using BankAPI.Domain.Repository;
using MongoDB.Bson;

namespace BankAPI.Domain.Business.Services
{
    public class BankService : IBankService
    {
        private readonly BankDbContext _dbContext;
        private readonly BankRepo bankRepo;

        public BankService(BankDbContext dbContext)
        {
            _dbContext = dbContext;
            this.bankRepo = new BankRepo(_dbContext);
        }

        public List<Banks> GetAllBanks()
        {
            return bankRepo.GetAll();
        }

        public Banks GetBanksById(ObjectId id)
        {
            return bankRepo.GetBanksById(id);
        }
    }
}
