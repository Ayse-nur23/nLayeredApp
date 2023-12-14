using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;

namespace Business.Abstract;

public interface ICustomerService
{
    Task<IPaginate<GetListCustomerResponse>> GetListAsync(PageRequest pageRequest);

    // Task<GetListProductResponse> GetAsync(int id);
    Task<CreatedCustomerResponse> Add(CreateCustomerRequest createCustomerRequest);
    Task<DeletedCustomerResponse> Delete(DeleteCustomerRequest deleteCustomerRequest);
    Task<UpdatedCustomerResponse> Update(UpdateCustomerRequest updateCustomerRequest);
    
}