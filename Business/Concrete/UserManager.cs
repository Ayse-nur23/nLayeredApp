using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using DataAccess.Abstract;
using Core.Entities.Concrete;
using Business.Dtos.Users;
using Business.Rules;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using Microsoft.EntityFrameworkCore;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Business.BusinessAspects.Autofac;

namespace Business.Concrete;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;
    private readonly IMapper _mapper;
    private readonly UserBusinessRules _userBusinessRules;

    public UserManager(IUserDal userDal, IMapper mapper, UserBusinessRules userBusinessRules, IFileUploadService fileUploadService)
    {
        _userDal = userDal;
        _mapper = mapper;
        _userBusinessRules = userBusinessRules;
    }

    public async Task<UserAuth> Add(UserAuth userAuth)
    {
        User user = _mapper.Map<User>(userAuth);
        User userCreated = await _userDal.AddAsync(user);
        UserAuth userAuthMap = _mapper.Map<UserAuth>(userCreated);
        return userAuthMap;
    }
    public async Task<UpdatedUserResponse> Update(UpdateUserRequest updateUserRequest)
    {
        await _userBusinessRules.VerifyTCKN(updateUserRequest);
        User? user = await _userDal.GetAsync(u => u.Id == updateUserRequest.Id);
        user = _mapper.Map(updateUserRequest, user);
        User userUpdated = await _userDal.UpdateAsync(user);
        UpdatedUserResponse updatedUserResponse = _mapper.Map<UpdatedUserResponse>(userUpdated);

        return updatedUserResponse;
    }
    public async Task<UserAuth> GetByMail(string email)
    {
        var result = await _userDal.GetAsync(u => u.Email == email);
        UserAuth userAuth = _mapper.Map<UserAuth>(result);
        return userAuth;
    }
    public async Task<UserAuth> GetById(Guid id)
    {
        User? user = await _userDal.GetAsync(i => i.Id == id);
        UserAuth userAuth = _mapper.Map<UserAuth>(user);
        return userAuth;
    }

    [SecuredOperation("admin")]
    public async Task<IPaginate<GetListUserResponse>> GetListAsync(PageRequest pageRequest)
    {
        var data = await _userDal.GetListAsync(index: pageRequest.PageIndex, size: pageRequest.PageSize);

        var result = _mapper.Map<Paginate<GetListUserResponse>>(data);
        return result;
    }

    public async Task<DeletedUserResponse> Delete(DeleteUserRequest deleteUserRequest)
    {

        User user = _mapper.Map<User>(deleteUserRequest);

        User userDeleted = await _userDal.DeleteAsync(user);

        DeletedUserResponse deletedUserResponse = _mapper.Map<DeletedUserResponse>(userDeleted);

        return deletedUserResponse; ;
    }
}