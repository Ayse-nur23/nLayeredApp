using AutoMapper;
using Business.Abstract;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Business.Rules;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete;

public class ProductManager : IProductService
{
    IProductDal _productDal;
    IMapper _mapper;
    ProductBusinessRules _productBusinessRules;
    public ProductManager(IProductDal productDal, IMapper mapper, ProductBusinessRules productBusinessRules)
    {
        _productDal = productDal;
        _mapper = mapper;
        _productBusinessRules = productBusinessRules;
    }
    public async Task<CreatedProductResponse> Add(CreateProductRequest createProductRequest)
    {
        await _productBusinessRules.EachCategoryCanContainMax20Products(createProductRequest.CategoryId);
        Product product = _mapper.Map<Product>(createProductRequest);

        Product createdProduct = await _productDal.AddAsync(product);
        CreatedProductResponse createdProductResponse = _mapper.Map<CreatedProductResponse>(createdProduct);
        return createdProductResponse;
    }

    public async Task<DeletedProductResponse> Delete(DeleteProductRequest deleteProductRequest)
    {
        Product product = _mapper.Map<Product>(deleteProductRequest);
        Product deletedProduct = await _productDal.DeleteAsync(product);
        DeletedProductResponse deletedProductResponse = _mapper.Map<DeletedProductResponse>(deletedProduct);
        return deletedProductResponse;
    }


    public Task<GetListProductResponse> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IPaginate<GetListProductResponse>> GetListAsync(PageRequest pageRequest)
    {
        var data = await _productDal.GetListAsync(
            include: p => p.Include(p => p.Category),
            index: pageRequest.PageIndex,
            size: pageRequest.PageSize
            );
        var result = _mapper.Map<Paginate<GetListProductResponse>>(data);
        return result;
    }

    public async Task<UpdatedProductResponse> Update(UpdateProductRequest updateProductRequest)
    {
        Product product = _mapper.Map<Product>(updateProductRequest);
        Product updatedProduct = await _productDal.UpdateAsync(product);
        UpdatedProductResponse updatedProductResponse = _mapper.Map<UpdatedProductResponse>(updatedProduct);
        return updatedProductResponse;
    }
}
