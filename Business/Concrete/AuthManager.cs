using AutoMapper;
using Business.Abstract;
using Business.Dtos.Requests;
using Business.DTOs.Users;
using Business.Messages;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Entities.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{

    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        IUserOperationClaimService _userOperationClaimService;
        IMapper _mapper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IMapper mapper, IUserOperationClaimService userOperationClaimService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
            _userOperationClaimService = userOperationClaimService;
        }

        public async Task<UserAuth> Register(UserForRegisterRequest userForRegisterRequest, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);               
            UserAuth userAuth = _mapper.Map<UserAuth>(userForRegisterRequest);
            userAuth.PasswordHash = passwordHash;
            userAuth.PasswordSalt = passwordSalt;
            await _userService.Add(userAuth);
            return userAuth;

        }

        public Task<UserAuth> Login(UserForLoginRequest userForLoginRequest)
        {
            var userToCheck = _userService.GetByMail(userForLoginRequest.Email);
            if (userToCheck == null)
            {
                throw new BusinessException(BusinessMesaages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginRequest.Password, userToCheck.Result.PasswordHash, userToCheck.Result.PasswordSalt))
            {
                throw new BusinessException(BusinessMesaages.PasswordError);
            }

            return userToCheck;
        }

        public Task UserExists(string email)
        {
            if (_userService.GetByMail(email).Result != null)
            {
               throw new BusinessException(BusinessMesaages.UserAlreadyExists);
            }
            return null ;
        }

        public AccessToken CreateAccessToken(UserAuth userAuth)
        {
            var claims = _userOperationClaimService.GetClaims(userAuth.Id).Result;
            var accessToken = _tokenHelper.CreateToken(userAuth, claims);
            return accessToken;
        }

    }
}
