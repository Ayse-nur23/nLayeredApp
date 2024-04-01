using Business.Abstract;
using Business.Dtos.Users;
using Business.Messages;
using Core.Business.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Entities.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Core.Utilities.Verification.TCKN;
using DataAccess.Abstract;

namespace Business.Rules;

public class UserBusinessRules : BaseBusinessRules
{
    private readonly IUserDal _userDal;
    private readonly IFileUploadDal _fileUploadDal;
    private readonly IVerificationService _verificationService;


    public UserBusinessRules(IUserDal userDal, IFileUploadDal fileUploadDal, IVerificationService verificationService)
    {
        _userDal = userDal;
        _fileUploadDal = fileUploadDal;
        _verificationService = verificationService;
    }

    public async Task FileMustBeInImageFormat(Guid Id)
    {
        var image = await _fileUploadDal.GetAsync(f => f.Id == Id);
        List<string> extensions = new() { ".jpg", ".png", ".jpeg", ".webp" };

        string extension = Path.GetExtension(image.FileName).ToLower();
        if (!extensions.Contains(extension))
            throw new BusinessException("Unsupported format");
        await Task.CompletedTask;
    }


    public async Task VerifyTCKN(UpdateUserRequest updateUserRequest)
    {
        var verificationResult = await _verificationService.VerifyTCKN(long.Parse(updateUserRequest.IdentityNumber),
            updateUserRequest.FirstName, updateUserRequest.LastName, updateUserRequest.BirthDate.Year);

        if (!verificationResult)
            throw new BusinessException("TC Kimlik numarası doğrulanamadı.");
    }
}