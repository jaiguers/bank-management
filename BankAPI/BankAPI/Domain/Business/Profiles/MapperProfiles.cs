using AutoMapper;
using BankAPI.CrossCutting.AppModelDtos;
using BankAPI.Domain.Models;

namespace BankAPI.Domain.Business.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Cards, CardsDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Banks, BankDTO>().ReverseMap();
            CreateMap<Amount, AmountDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            CreateMap<CreateCardsDTO, Cards>();
        }
    }
}
