using AutoMapper;
using Business.Abstract;
using Business.Dtos.Customers;
using Business.Rules;
using Core.Aspects.Autofac.Caching;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete;

public class CustomerManager : ICustomerService
{
    private readonly ICustomerDal _customerDal;
    private readonly IMapper _mapper;
    private readonly CustomerBusinessRules _customerBusinessRules;

    public CustomerManager(ICustomerDal customerDal, CustomerBusinessRules customerBusinessRules, IMapper mapper)
    {
        _customerDal = customerDal;
        _customerBusinessRules = customerBusinessRules;
        _mapper = mapper;
    }

    public async Task<CreatedCustomerResponse> Add(CreateCustomerRequest createCustomerRequest)
    {
        await _customerBusinessRules.EachCityCanContainMax10Customers(createCustomerRequest.City);
        await _customerBusinessRules.IsExistsContactName(createCustomerRequest.ContactName);
        Customer customer = _mapper.Map<Customer>(createCustomerRequest);
        Customer createdCustomer = await _customerDal.AddAsync(customer);
        CreatedCustomerResponse createdCustomerResponse = _mapper.Map<CreatedCustomerResponse>(createdCustomer);
        return createdCustomerResponse;

    }

    public async Task<DeletedCustomerResponse> Delete(DeleteCustomerRequest deleteCustomerRequest)
    {
        Customer customer = _mapper.Map<Customer>(deleteCustomerRequest);
        Customer deletedCustomer = await _customerDal.DeleteAsync(customer, true);
        DeletedCustomerResponse deletedCustomerResponse  = _mapper.Map<DeletedCustomerResponse>(deletedCustomer);
        return deletedCustomerResponse;
    }

    [CacheAspect]
    public async Task<GetListCustomerResponse> GetAsync(Guid id)
    {
        var data = await _customerDal.GetAsync(predicate: c=>c.Id == id);
        GetListCustomerResponse getListCustomerResponse = _mapper.Map<GetListCustomerResponse>(data);
        return getListCustomerResponse;
    }

    [CacheAspect]
    public async Task<IPaginate<GetListCustomerResponse>> GetListAsync(PageRequest pageRequest)
    {
        var data = await _customerDal.GetListAsync(index: pageRequest.PageIndex,
           size: pageRequest.PageSize);
        var result = _mapper.Map<Paginate<GetListCustomerResponse>>(data);
        return result;
    }

    public async Task<UpdatedCustomerResponse> Update(UpdateCustomerRequest updateCustomerRequest)
    {
        Customer customer = _mapper.Map<Customer>(updateCustomerRequest);
        Customer updatedCustomer = await _customerDal.UpdateAsync(customer);
        UpdatedCustomerResponse updatedCustomerResponse = _mapper.Map<UpdatedCustomerResponse>(updatedCustomer);
        return updatedCustomerResponse;
    }
}
