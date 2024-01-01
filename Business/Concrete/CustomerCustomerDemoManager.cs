using AutoMapper;
using Business.Abstract;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete;

public class CustomerCustomerDemoManager : ICustomerCustomerDemoService
{
    ICustomerCustomerDemoDal _customerCustomerDemo;
    IMapper _mapper;

    public CustomerCustomerDemoManager(ICustomerCustomerDemoDal customerCustomerDemo, IMapper mapper)
    {
        _customerCustomerDemo = customerCustomerDemo;
        _mapper = mapper;
    }

    public async Task<CreatedCustomerCustomerDemoResponse> Add(CreateCustomerCustomerDemoRequest createCustomerCustomerDemoRequest)
    {
        CustomerCustomerDemo customerCustomerDemo = _mapper.Map<CustomerCustomerDemo>(createCustomerCustomerDemoRequest);
        CustomerCustomerDemo createdCustomerCustomerDemo = await _customerCustomerDemo.AddAsync(customerCustomerDemo);
        CreatedCustomerCustomerDemoResponse createdCustomerCustomerDemoResponse = _mapper.Map<CreatedCustomerCustomerDemoResponse>(createdCustomerCustomerDemo);
        return createdCustomerCustomerDemoResponse;
    }

    public async Task<DeletedCustomerCustomerDemoResponse> Delete(DeleteCustomerCustomerDemoRequest deleteCustomerCustomerDemoRequest)
    {
        CustomerCustomerDemo customerCustomerDemo = _mapper.Map<CustomerCustomerDemo>(deleteCustomerCustomerDemoRequest);
        CustomerCustomerDemo deletedCustomerCustomerDemo = await _customerCustomerDemo.DeleteAsync(customerCustomerDemo);
        DeletedCustomerCustomerDemoResponse deletedCustomerCustomerDemoResponse = _mapper.Map<DeletedCustomerCustomerDemoResponse>(deletedCustomerCustomerDemo);
        return deletedCustomerCustomerDemoResponse;
    }

    public async Task<IPaginate<GetListCustomerCustomerDemoResponse>> GetListAsync(PageRequest pageRequest)
    {
        var data = await _customerCustomerDemo.GetListAsync(include: c=>c.Include(c=>c.Customer).Include(c=>c.CustomerDemographic),
            index: pageRequest.PageIndex,
          size: pageRequest.PageSize);
        var result = _mapper.Map<Paginate<GetListCustomerCustomerDemoResponse>>(data);
        return result;
    }

    public async Task<UpdatedCustomerCustomerDemoResponse> Update(UpdateCustomerCustomerDemoRequest updateCustomerCustomerDemoRequest)
    {
        CustomerCustomerDemo customerCustomerDemo = _mapper.Map<CustomerCustomerDemo>(updateCustomerCustomerDemoRequest);
        CustomerCustomerDemo updatedCustomerCustomerDemo = await _customerCustomerDemo.DeleteAsync(customerCustomerDemo);
        UpdatedCustomerCustomerDemoResponse updatedCustomerCustomerDemoResponse = _mapper.Map<UpdatedCustomerCustomerDemoResponse>(updatedCustomerCustomerDemo);
        return updatedCustomerCustomerDemoResponse;
    }
}
