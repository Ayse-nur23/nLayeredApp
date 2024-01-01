using Core.DataAccess.Repositories;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;

namespace DataAccess.Concrete;

public class EfCustomerCustomerDemoDal : EfRepositoryBase<CustomerCustomerDemo, string, NorthwindContext>, ICustomerCustomerDemoDal
{
    public EfCustomerCustomerDemoDal(NorthwindContext context) : base(context)
    {
    }
}