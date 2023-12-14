using Business.Messages;
using Core.Business.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using DataAccess.Abstract;
using DataAccess.Concrete;

namespace Business.Rules;

public class CustomerBusinessRules : BaseBusinessRules
{
    private readonly ICustomerDal _customerDal;

    public CustomerBusinessRules(ICustomerDal customerDal)
    {
        _customerDal = customerDal;
    }

    public async Task EachCityCanContainMax10Customers(string city)
    {
        var result = await _customerDal.GetListAsync(predicate: p => p.City == city, size: 15);
        if (result.Count >= 10)
        {
            throw new BusinessException(BusinessMesaages.CityCustomerLimit);
        }
    }

    public async Task IsExistsContactName(string contactName)
    {
        var result = await _customerDal.GetListAsync(predicate: p => p.ContactName == contactName);
        if (result != null)
        {
            throw new BusinessException(BusinessMesaages.ExistsContactName);
        }
    }

}