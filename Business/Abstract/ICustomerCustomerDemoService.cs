using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;

namespace Business.Abstract;

public interface ICustomerCustomerDemoService
{
    Task<IPaginate<GetListCustomerCustomerDemoResponse>> GetListAsync(PageRequest pageRequest);
    Task<CreatedCustomerCustomerDemoResponse> Add(CreateCustomerCustomerDemoRequest createCustomerCustomerDemoRequest);
    Task<DeletedCustomerCustomerDemoResponse> Delete(DeleteCustomerCustomerDemoRequest deleteCustomerCustomerDemoRequest);
    Task<UpdatedCustomerCustomerDemoResponse> Update(UpdateCustomerCustomerDemoRequest updateCustomerCustomerDemoRequest);
}