using Core.DataAccess.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface ICustomerCustomerDemoDal : IRepository<CustomerCustomerDemo, string>, IAsyncRepository<CustomerCustomerDemo, string>
{
}