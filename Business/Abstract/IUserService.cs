using Business.Dtos.Requests;
using Business.DTOs.Users;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        //List<OperationClaim> GetClaims(User user);
        //List<GetOperationClaimResponse> GetClaims(CreatedUserResponse createdUserResponse);

        //void Add(User user);
        Task<UserAuth> Add(UserAuth userAuth);
        //User GetByMail(string email);
        Task<UserAuth> GetByMail(string email);


    }
}
