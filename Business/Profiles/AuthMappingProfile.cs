using AutoMapper;
using Business.Dtos.Users;
using Core.DataAccess.Dynamic;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace Business.Profiles;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<User, UserAuth>().ReverseMap();

        CreateMap<User, UserForRegisterRequest>().ReverseMap();
        CreateMap<User, UserForLoginRequest>().ReverseMap();

        CreateMap<UserAuth, UserForRegisterRequest>().ReverseMap();
        CreateMap<UserAuth, UserForLoginRequest>().ReverseMap();
    }
}
