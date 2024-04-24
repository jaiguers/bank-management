using BankAPI.CrossCutting.AppModelDtos;

namespace BankAPI.Domain.Business.Interface
{
    public interface ICardService
    {
        CardsDTO CreateCard(CreateCardsDTO dto);
        List<CardsDTO> GetCardByUserId(Guid id);
        bool DisableCard(string id);
        CardsDTO UpdateCard(CardsDTO dto);
        CardsDTO GetById(string id);
        CardsDTO ActivateCard(string id);
    }
}
