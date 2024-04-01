using Core.DataAccess.Repositories;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;

namespace DataAccess.Concrete;

public class EfFileUploadDal : EfRepositoryBase<FileUpload, Guid, NorthwindCloneContext>, IFileUploadDal
{
    public EfFileUploadDal(NorthwindCloneContext context) : base(context)
    {
    }
}