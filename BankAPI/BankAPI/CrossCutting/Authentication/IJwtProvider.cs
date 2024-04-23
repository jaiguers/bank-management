using BankAPI.CrossCutting.AppModelDtos;

namespace BankAPI.CrossCutting.Authentication
{
    public interface IJwtProvider
    {
        string Generate(AuthDTO userAuth);
    }
}
