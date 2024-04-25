using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using BankAPI.CrossCutting.AppModelDtos;
using BankAPI.CrossCutting.Enumerators;
using BankAPI.Domain.Business.Interface;
using BankAPI.Domain.Business.Profiles;
using BankAPI.Domain.Models;
using BankAPI.Domain.Repository;
using SharpCompress.Common;
using System.Security.AccessControl;

namespace BankAPI.Domain.Business.Services
{
    public class CardService : ICardService
    {
        private readonly CardsRepo cardsRepo;
        private readonly IMapper mapper;

        public CardService(BankDbContext dbContext)
        {
            cardsRepo = new CardsRepo(dbContext);

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.AddProfile<MapperProfiles>();
            });

            this.mapper = new Mapper(mapConfig);
        }

        public CardsDTO CreateCard(CreateCardsDTO dto)
        {
            Cards entity = mapper.Map<Cards>(dto);

            entity.CardNumber = GetCardNumber(dto.Provider);
            entity.Balance = "0.0";
            entity.CreatedAt = DateTime.Now;
            entity.ActivatedAt = DateTime.Now;
            entity.ExpirationDate = GetExpirationDate();
            entity.CVV = GetCVV();
            entity.Status = StatesEnum.Pending;
            entity.amounts = new List<Amount>();

            cardsRepo.Create(entity);

            var cardDto = mapper.Map<CardsDTO>(entity);
            cardDto.Id = entity.Id.ToString();

            return cardDto;
        }

        public bool DisableCard(string id)
        {
            throw new NotImplementedException();
        }

        public List<CardsDTO> GetCardByUserId(Guid id)
        {
            var cards = cardsRepo.GetByUserId(id);

            return mapper.Map<List<CardsDTO>>(cards);
        }

        public CardsDTO UpdateCard(CardsDTO dto)
        {
            var card = mapper.Map<Cards>(dto);
            cardsRepo.Update(card);

            return dto;
        }

        public CardsDTO ActivateCard(string id)
        {

            var card = cardsRepo.Activate(id);

            return mapper.Map<CardsDTO>(card);
        }

        public CardsDTO GetById(string id)
        {
            var card = cardsRepo.GetById(id);

            return mapper.Map<CardsDTO>(card);
        }

        private static string GetCVV()
        {
            string cvv = string.Empty;
            Random rnd = new();

            for (int i = 0; i < 3; i++)
            {
                int randNum = rnd.Next(10);
                cvv += randNum.ToString();
            }

            return cvv;
        }

        private string GetExpirationDate()
        {
            var date = DateTime.Now.AddYears(3);

            return $"{date.Month}/{date.Year}";
        }

        private string GetCardNumber(string type)
        {
            string carNumber = string.Empty;
            Random random = new Random();

            string tempNumber = Convert.ToString((long)random.NextDouble() * 9_000_000_000L + 1_000_000_000L);

            switch (type)
            {
                case CardProviderEnum.Visa:
                    carNumber = $"497{tempNumber}";
                    break;
                case CardProviderEnum.MasterCard:
                    carNumber = $"510{tempNumber}";
                    break;
                case CardProviderEnum.Diners:
                    carNumber = $"360{tempNumber}";
                    break;
            }

            return carNumber;
        }
    }
}
