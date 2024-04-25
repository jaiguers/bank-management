using BankAPI.CrossCutting.Enumerators;
using BankAPI.Domain.Models;
using MongoDB.Bson;
using SharpCompress.Common;
using System.Linq;

namespace BankAPI.Domain.Repository
{
    public class CardsRepo
    {
        private readonly BankDbContext _dbContext;

        public CardsRepo(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Cards Create(Cards entity)
        {
            _dbContext.Cards.Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public Cards Update(Cards entity)
        {
            _dbContext.Cards.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Cards Activate(string id)
        {
            var entity = GetById(id);
            entity.Status = StatesEnum.Active;
            entity.ActivatedAt = DateTime.Now;

            _dbContext.Cards.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public bool Delete(Cards entity)
        {
            _dbContext.Cards.Remove(entity);
            _dbContext.SaveChanges();

            return true;
        }

        public Cards? GetById(string id)
        {
            return _dbContext.Cards.Where(j => j.Id.ToString().Equals(id)).FirstOrDefault();
        }

        public List<Cards> GetByUserId(Guid id)
        {
            var newList = new List<Cards>();
            var cardsList = _dbContext.Cards.Where(j => j.UserId == id);
            if (cardsList != null)
            {
                foreach (var card in cardsList)
                {
                    card.amounts ??= new List<Amount>();
                    newList.Add(card);
                }
            }
            return newList;
        }
    }
}
