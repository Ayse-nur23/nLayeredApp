using AutoMapper;
using Business.Abstract;
using Business.DTOs.Users;
using Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Dtos.Requests;
using Core.Entities.Concrete;

namespace Business.Concretes
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        IMapper _mapper;

        public UserManager(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }

        //public List<OperationClaim> GetClaims(User user)
        //{
        //    return _userDal.GetClaims(user);
        //}

        public async Task<UserAuth> Add(UserAuth userAuth)
        {
            User user = _mapper.Map<User>(userAuth);
            User userCreated = await _userDal.AddAsync(user);
            UserAuth userAuthMap = _mapper.Map<UserAuth>(userCreated);
            return userAuthMap;
        }

        public async Task<UserAuth> GetByMail(string email)
        {
            var result = await _userDal.GetAsync(u => u.Email == email);
            UserAuth userAuth = _mapper.Map<UserAuth>(result);
            return userAuth;
        }
    }
}
