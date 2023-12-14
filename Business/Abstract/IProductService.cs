using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract;

public interface IProductService
{
    Task<IPaginate<GetListProductResponse>> GetListAsync(PageRequest pageRequest);
    Task<GetListProductResponse> GetAsync(int id);
    Task<CreatedProductResponse> Add(CreateProductRequest createProductRequest);
    Task<DeletedProductResponse> Delete(DeleteProductRequest deleteProductRequest);
    Task<UpdatedProductResponse> Update(UpdateProductRequest updateProductRequest);
   

}
