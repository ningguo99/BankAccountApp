using AutoMapper;
using BankAccountApi.DTOs;
using BankAccountApi.Entities;

namespace BankAccountApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, ReturnedUserDto>();
            CreateMap<CreateUserDto, AppUser>();
            CreateMap<BankAccount, ReturnedBankAccountDto>();
            CreateMap<AddBankAccountDto, BankAccount>();
            
        }
    }
}