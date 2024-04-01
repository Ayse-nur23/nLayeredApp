using AutoMapper;
using Business.Abstract;
using Business.Dtos.RefreshTokens;
using Business.Rules;
using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete;

public class RefreshTokenManager : IRefreshTokenService
{
    private readonly IUserService _userService;
    private readonly IUserOperationClaimService _userOperationClaimService;
    private readonly ITokenHelper _tokenHelper;
    private readonly IRefreshTokenDal _refreshTokenDal;
    private readonly IMapper _mapper;
    private readonly TokenOptions _tokenOptions;
    private readonly RefreshTokenBusinessRules _refreshTokenBusinessRules;


    public RefreshTokenManager(IUserService userService, IRefreshTokenDal refreshTokenDal, IMapper mapper, RefreshTokenBusinessRules refreshTokenBusinessRules,
        ITokenHelper tokenHelper, TokenOptions tokenOptions, IUserOperationClaimService userOperationClaimService)
    {
        _userService = userService;
        _refreshTokenDal = refreshTokenDal;
        _mapper = mapper;
        _refreshTokenBusinessRules = refreshTokenBusinessRules;
        _tokenHelper = tokenHelper;
        _tokenOptions = tokenOptions;
        _userOperationClaimService = userOperationClaimService;
    }

    public async Task<RefreshTokenResponse> RefreshAccessToken(string RefreshToken, string IpAddress)
    {
        var refreshToken = await GetRefreshTokenByToken(RefreshToken);
        await _refreshTokenBusinessRules.RefreshTokenMustExist(refreshToken);

        if (refreshToken!.Revoked != null)
            await RevokeDescendantRefreshTokens(refreshToken, IpAddress, reason: $"Attempted reuse of revoked ancestor token: {refreshToken.Token}");

        await _refreshTokenBusinessRules.RefreshTokenShouldBeActive(refreshToken);

        var user = await _userService.GetById(refreshToken.UserId);
        await _refreshTokenBusinessRules.UserShouldBeExistsWhenSelected(user);

        RefreshToken newRefreshToken = await RotateRefreshToken(user: user!, refreshToken, IpAddress);
        RefreshToken addedRefreshToken = await AddRefreshToken(newRefreshToken);
        await DeleteOldRefreshTokens(refreshToken.UserId);

        AccessToken createdAccessToken = await CreateAccessToken(user!);

        RefreshTokenResponse refreshedTokensResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken};
        return refreshedTokensResponse;
    }

    public async Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress, string reason)
    {
        RefreshToken? childToken = await _refreshTokenDal.GetAsync(predicate: r => r.Token == refreshToken.ReplacedByToken);

        if (childToken?.Revoked != null && childToken.Expires <= DateTime.UtcNow)
            await RevokeRefreshToken(childToken, ipAddress, reason);
        else
            await RevokeDescendantRefreshTokens(refreshToken: childToken!, ipAddress, reason);
    }

    public async Task<RefreshToken> RotateRefreshToken(UserAuth user, RefreshToken refreshToken, string ipAddress)
    {
        RefreshToken newRefreshToken = await CreateRefreshToken(user, ipAddress);
        await RevokeRefreshToken(refreshToken, ipAddress, reason: "Replaced by new token", newRefreshToken.Token);
        return newRefreshToken;
    }

    public async Task DeleteOldRefreshTokens(Guid userId)
    {
        List<RefreshToken> refreshTokens = await _refreshTokenDal.Query().AsNoTracking()
            .Where(
                r =>
                    r.UserId == userId
                    && r.Revoked == null
                && r.Expires >= DateTime.UtcNow
                    && r.CreatedDate.AddDays(_tokenOptions.RefreshTokenTTL) <= DateTime.UtcNow
            )
            .ToListAsync();

        await _refreshTokenDal.DeleteRangeAsync(refreshTokens);
    }
    public async Task RevokeRefreshToken(RefreshToken refreshToken, string ipAddress, string? reason = null, string? replacedByToken = null)
    {
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReasonRevoked = reason;
        refreshToken.ReplacedByToken = replacedByToken;
        await _refreshTokenDal.UpdateAsync(refreshToken);
    }
    public async Task<RefreshToken?> GetRefreshTokenByToken(string token)
    {
        RefreshToken? refreshToken = await _refreshTokenDal.GetAsync(predicate: r => r.Token == token);
        return refreshToken;
    }
    public async Task<RevokedTokenResponse> RevokedToken(string Token, string IPAddress)
    {
        RefreshToken? refreshToken = await GetRefreshTokenByToken(Token);
        await _refreshTokenBusinessRules.RefreshTokenShouldBeExists(refreshToken);
        await _refreshTokenBusinessRules.RefreshTokenShouldBeActive(refreshToken!);

        await RevokeRefreshToken(refreshToken: refreshToken!, IPAddress, reason: "Revoked without replacement");

        RevokedTokenResponse revokedTokenResponse = _mapper.Map<RevokedTokenResponse>(refreshToken);
        return revokedTokenResponse;
    }
    public async Task<RefreshToken> CreateRefreshToken(UserAuth userAuth, string ipAddress)
    {
        RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(userAuth, ipAddress);
        return await Task.FromResult(refreshToken);
    }


    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshToken addedRefreshToken = await _refreshTokenDal.AddAsync(refreshToken);
        return addedRefreshToken;
    }

    public async Task<AccessToken> CreateAccessToken(UserAuth userAuth)
    {
        var claims = await _userOperationClaimService.GetClaims(userAuth.Id);
        var accessToken = await _tokenHelper.CreateToken(userAuth, claims);
        return accessToken;
    }
}
