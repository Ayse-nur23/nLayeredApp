using Core.DataAccess.Repositories;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete;

public class EfProductDal : EfRepositoryBase<Product, Guid, NorthwindCloneContext>, IProductDal
{
    public EfProductDal(NorthwindCloneContext context) : base(context)
    {
    }
}
