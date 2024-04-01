using Business.Dtos.Users;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<UserAuth> Add(UserAuth userAuth);
        Task<UpdatedUserResponse> Update(UpdateUserRequest updateUserRequest);
        Task<UserAuth> GetByMail(string email);
        Task<UserAuth> GetById(Guid id);
        Task<DeletedUserResponse> Delete(DeleteUserRequest deleteUserRequest);

        Task<IPaginate<GetListUserResponse>> GetListAsync(PageRequest pageRequest);

    }
}
