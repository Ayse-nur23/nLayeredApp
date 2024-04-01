using Business.Dtos.Products;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using Entities.Concrete;

namespace Business.Abstract;

public interface IProductService
{
    Task<IPaginate<GetListProductResponse>> GetListAsync(PageRequest pageRequest);
    Task<GetListProductResponse> GetAsync(Guid id);
    Task<CreatedProductResponse> Add(CreateProductRequest createProductRequest);
    Task<DeletedProductResponse> Delete(DeleteProductRequest deleteProductRequest);
    Task<UpdatedProductResponse> Update(UpdateProductRequest updateProductRequest);

    Task TransactionalOperation();
}