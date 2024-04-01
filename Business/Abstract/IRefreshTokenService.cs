using Business.Dtos.RefreshTokens;
using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;

namespace Business.Abstract;

public interface IRefreshTokenService
{
    Task<RefreshTokenResponse> RefreshAccessToken(string RefreshToken, string IPAddress);
    Task<RevokedTokenResponse> RevokedToken(string RefreshToken, string IPAddress);
    Task<AccessToken> CreateAccessToken(UserAuth userAuth);
    Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken> CreateRefreshToken(UserAuth userAuth, string ipAddress);
}
