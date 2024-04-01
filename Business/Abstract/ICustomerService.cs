using Business.Dtos.Customers;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;

namespace Business.Abstract;

public interface ICustomerService
{
    Task<IPaginate<GetListCustomerResponse>> GetListAsync(PageRequest pageRequest);

     Task<GetListCustomerResponse> GetAsync(Guid id);
    Task<CreatedCustomerResponse> Add(CreateCustomerRequest createCustomerRequest);
    Task<DeletedCustomerResponse> Delete(DeleteCustomerRequest deleteCustomerRequest);
    Task<UpdatedCustomerResponse> Update(UpdateCustomerRequest updateCustomerRequest);
    
}