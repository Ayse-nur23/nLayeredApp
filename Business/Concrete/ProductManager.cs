using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Dtos.Products;
using Business.Rules;
using Business.Validators;
using Business.Validators.Products;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace Business.Concrete;

//[PerformanceAspect(4)]

public class ProductManager : IProductService
{
    private readonly IProductDal _productDal;
    private readonly IFileUploadService _fileUploadService;
    private readonly IMapper _mapper;
    private readonly ProductBusinessRules _productBusinessRules;

    public ProductManager(IProductDal productDal, IMapper mapper, ProductBusinessRules productBusinessRules, IFileUploadService fileUploadService)
    {
        _productDal = productDal;
        _mapper = mapper;
        _productBusinessRules = productBusinessRules;
        _fileUploadService = fileUploadService;
    }

    //[SecuredOperation("admin, user_admin")]
    [ValidationAspect(typeof(CreateProductRequestValidator))]
    // [CacheRemoveAspect]
    public async Task<CreatedProductResponse> Add(CreateProductRequest createProductRequest)
    {
        //  await _productBusinessRules.EachCategoryCanContainMax20Products(createProductRequest.CategoryId);
        Product product = _mapper.Map<Product>(createProductRequest);
        Product createdProduct = await _productDal.AddAsync(product);
        CreatedProductResponse createdProductResponse = _mapper.Map<CreatedProductResponse>(createdProduct);
        return createdProductResponse;
    }

    public async Task<DeletedProductResponse> Delete(DeleteProductRequest deleteProductRequest)
    {
        //Product product = _mapper.Map<Product>(deleteProductRequest);
        Product? product = await _productDal.GetAsync(p => p.Id == deleteProductRequest.Id);
        await _productDal.DeleteAsync(product);
        DeletedProductResponse deletedProductResponse = _mapper.Map<DeletedProductResponse>(product);
        return deletedProductResponse;
    }

    [CacheAspect]
    public async Task<GetListProductResponse> GetAsync(Guid id)
    {
        var data = await _productDal.GetAsync(
            include: p => p.Include(p => p.Category),
            predicate: p => p.Id == id
            );
        var result = _mapper.Map<GetListProductResponse>(data);
        return result;
    }

    [CacheAspect]
    [SecuredOperation("admin, user_admin", Priority = 1)]
    public async Task<IPaginate<GetListProductResponse>> GetListAsync(PageRequest pageRequest)
    {
        Thread.Sleep(5000);

        var data = await _productDal.GetListAsync(
            include: p => p.Include(p => p.Category),
            index: pageRequest.PageIndex,
            size: pageRequest.PageSize
            );
        var result = _mapper.Map<Paginate<GetListProductResponse>>(data);
        return result;
    }

    [TransactionScopeAspect]
    public async Task TransactionalOperation()
    {
        Guid categoryId = new Guid("6bd76f3f-92a3-45e8-211f-08dc1909eb2b");
        Guid id = new Guid("e01dee8e-664c-4ac1-24bf-08dc190a6d05");

        Update(new UpdateProductRequest
        { CategoryId = categoryId, Id = id, ProductName = "Test PN4", QuantityPerUnit = "Test1", UnitPrice = 150, UnitsInStock = 10 });
        Add(new CreateProductRequest
        { CategoryId = categoryId, ProductName = "Test PN3", UnitsInStock = 100, QuantityPerUnit = "Test", UnitPrice = 10 });
    }

    // [ValidationAspect(typeof(UpdateProductRequestValidator))]
    public async Task<UpdatedProductResponse> Update(UpdateProductRequest updateProductRequest)
    {
        Product product = await _productDal.GetAsync(u => u.Id == updateProductRequest.Id);
        _mapper.Map(updateProductRequest, product);
        Product updatedProduct = await _productDal.UpdateAsync(product);
        UpdatedProductResponse updatedProductResponse = _mapper.Map<UpdatedProductResponse>(updatedProduct);
        return updatedProductResponse;
    }


}