using Core.DataAccess.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IFileUploadDal : IRepository<FileUpload, Guid>, IAsyncRepository<FileUpload, Guid>
{
}
