using BankAPI.CrossCutting.AppModelDtos;

namespace BankAPI.Domain.Business.Interface
{
    public interface ITransactionService
    {
        CardsDTO CreateTransaction(AmountDTO data);
        List<CardsDTO> GetAllTransaction(Guid userId);
        CardsDTO GetTransactionByCard(string cardId);
    }
}
