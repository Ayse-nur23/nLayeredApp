using AutoMapper;
using Business.Abstract;
using Business.Dtos.RefreshTokens;
using Business.Dtos.Users;
using Business.Messages;
using Business.Rules;
using Core.Entities.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;

namespace Business.Concrete;


public class AuthManager : IAuthService
{
    private readonly IUserService _userService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IMapper _mapper;
    private readonly AuthBusinessRules _authBusinessRules;

    public AuthManager(IUserService userService, IMapper mapper, AuthBusinessRules authBusinessRules, IRefreshTokenService refreshTokenService)
    {
        _userService = userService;
        _mapper = mapper;
        _authBusinessRules = authBusinessRules;
        _refreshTokenService = refreshTokenService;
    }


    //[PerformanceAspect(1)]
    //[TransactionScopeAspect]
    public async Task<RefreshTokenResponse> Register(UserForRegisterRequest userForRegisterRequest, string password, string IpAddress)
    {
        await _authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(userForRegisterRequest.Email);

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        UserAuth userAuth = _mapper.Map<UserAuth>(userForRegisterRequest);
        userAuth.PasswordHash = passwordHash;
        userAuth.PasswordSalt = passwordSalt;
        var createdUser = await _userService.Add(userAuth);

        var createdAccessToken = await _refreshTokenService.CreateAccessToken(createdUser);
        RefreshToken createdRefreshToken = await _refreshTokenService.CreateRefreshToken(createdUser, IpAddress);
        RefreshToken addedRefreshToken = await _refreshTokenService.AddRefreshToken(createdRefreshToken);

        RefreshTokenResponse registeredDto = new()
        {
            RefreshToken = addedRefreshToken,
            AccessToken = createdAccessToken,
        };
        await _authBusinessRules.ThrowExceptionIfCreateAccessTokenIsNull(registeredDto);

        return registeredDto;
    }

    public async Task<RefreshTokenResponse> Login(UserForLoginRequest userForLoginRequest, string IpAddress)
    {
        var userToCheck = await _authBusinessRules.LoginInformationCheck(userForLoginRequest);

        var createdAccessToken = await _refreshTokenService.CreateAccessToken(userToCheck);
        RefreshToken createdRefreshToken = await _refreshTokenService.CreateRefreshToken(userToCheck, IpAddress);
        await _refreshTokenService.AddRefreshToken(createdRefreshToken);

        RefreshTokenResponse registeredDto = new()
        {
            RefreshToken = createdRefreshToken,
            AccessToken = createdAccessToken,
        };
        await _authBusinessRules.ThrowExceptionIfCreateAccessTokenIsNull(registeredDto);

        await _authBusinessRules.ThrowExceptionIfCreateAccessTokenIsNull(registeredDto);
        return registeredDto;
    }
    
}
