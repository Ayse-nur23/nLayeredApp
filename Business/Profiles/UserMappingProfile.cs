using AutoMapper;
using Business.Dtos.Users;
using Core.DataAccess.Dynamic;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace Business.Profiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserAuth>().ReverseMap();

        CreateMap<User, UserForRegisterRequest>().ReverseMap();
        CreateMap<User, UserForLoginRequest>().ReverseMap();

        CreateMap<UserAuth, UserForRegisterRequest>().ReverseMap();
        CreateMap<UserAuth, UserForLoginRequest>().ReverseMap();

        CreateMap<User, DeletedUserResponse>().ReverseMap();
        CreateMap<User, DeleteUserRequest>().ReverseMap();

        CreateMap<User, UpdatedUserResponse>().ReverseMap();
        CreateMap<User, UpdateUserRequest>().ReverseMap();

        CreateMap<User, GetListUserResponse>().ReverseMap();
        CreateMap<Paginate<User>,Paginate<GetListUserResponse>>().ReverseMap();
    }
}
  