using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using BankAPI.CrossCutting.AppModelDtos;
using BankAPI.CrossCutting.Enumerators;
using BankAPI.Domain.Business.Interface;
using BankAPI.Domain.Business.Profiles;
using BankAPI.Domain.Models;
using BankAPI.Domain.Repository;

namespace BankAPI.Domain.Business.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly CardsRepo cardRepo;
        private readonly IMapper mapper;

        public TransactionService(BankDbContext bankDb)
        {
            cardRepo = new CardsRepo(bankDb);

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.AddProfile<MapperProfiles>();
            });

            this.mapper = new Mapper(mapConfig);
        }

        public CardsDTO CreateTransaction(AmountDTO data)
        {
            var card = cardRepo.GetById(data.CardId);
            var amount = mapper.Map<Amount>(data);
            amount.Status = StatesEnum.Success;
            amount.CreatedAt = DateTime.Now;

            if (card.amounts == null || card.amounts.Count == 0)
                card.amounts = new List<Amount>();

            card.amounts.Add(amount);

            if (data.AmountType.Equals("Withdrawal"))
                card.Balance = $"{float.Parse(card.Balance) - float.Parse(data.TotalValue)}";
            else if (data.AmountType.Equals("Deposit"))
                card.Balance = $"{decimal.Parse(card.Balance) + decimal.Parse(data.TotalValue)}";

            cardRepo.Update(card);

            return mapper.Map<CardsDTO>(card);
        }

        public List<CardsDTO> GetAllTransaction(Guid userId)
        {
            var list = cardRepo.GetByUserId(userId);

            return mapper.Map<List<CardsDTO>>(list);
        }

        public CardsDTO GetTransactionByCard(string cardId)
        {
            var card = cardRepo.GetById(cardId);

            return mapper.Map<CardsDTO>(card);
        }
    }
}
