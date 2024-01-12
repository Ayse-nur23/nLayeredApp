using Core.DataAccess.Repositories;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;

namespace DataAccess.Concrete;

public class EfUserOperationClaimDal : EfRepositoryBase<UserOperationClaim, int, NorthwindContext>, IUserOperationClaimDal
{
    public EfUserOperationClaimDal(NorthwindContext context) : base(context)
    {
    }
}
