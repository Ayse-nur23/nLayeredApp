using Core.DataAccess.Repositories;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Context;

namespace DataAccess.Concrete;

public class EfRefreshTokenDal : EfRepositoryBase<RefreshToken, Guid, NorthwindCloneContext>, IRefreshTokenDal
{
    public EfRefreshTokenDal(NorthwindCloneContext context) : base(context)
    {
    }
}
