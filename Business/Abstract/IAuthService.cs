using Business.Dtos.RefreshTokens;
using Business.Dtos.Users;

namespace Business.Abstract;

public interface IAuthService
{
    Task<RefreshTokenResponse> Register(UserForRegisterRequest userForRegisterRequest, string password, string IpAddress);
    Task<RefreshTokenResponse> Login(UserForLoginRequest userForLoginRequest, string IpAddress);
}



