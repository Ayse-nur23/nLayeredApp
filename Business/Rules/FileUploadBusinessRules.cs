using Business.Messages;
using Core.Business.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rules;

public class FileUploadBusinessRules : BaseBusinessRules
{
    IFileUploadDal _fileUploadDal;

    public FileUploadBusinessRules(IFileUploadDal fileUploadDal)
    {
        _fileUploadDal = fileUploadDal;
    }

    public Task FileUploadsShouldExistWhenSelected(FileUpload? fileUpload)
    {
        if (fileUpload == null)
            throw new BusinessException(BusinessMessages.FileUploadsNotExists);
        return Task.CompletedTask;
    }

    public async Task FileUploadsIdShouldExistWhenSelected(Guid id)
    {
        FileUpload? fileUpload = await _fileUploadDal.GetAsync(
            predicate: fu => fu.Id == id,
            enableTracking: false
        );
        await FileUploadsShouldExistWhenSelected(fileUpload);
    }
}
