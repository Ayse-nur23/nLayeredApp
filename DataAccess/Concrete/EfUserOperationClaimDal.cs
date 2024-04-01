using Core.DataAccess.Repositories;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;

namespace DataAccess.Concrete;

public class EfUserOperationClaimDal : EfRepositoryBase<UserOperationClaim, Guid, NorthwindCloneContext>, IUserOperationClaimDal
{
    public EfUserOperationClaimDal(NorthwindCloneContext context) : base(context)
    {
    }
}
