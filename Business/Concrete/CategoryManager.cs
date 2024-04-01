using AutoMapper;
using Business.Abstract;
using Business.Dtos.Categories;
using Business.Rules;
using Business.Validators.Categories;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
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

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _categoryDal;
    private readonly IMapper _mapper;
    private readonly CategoryBusinessRules _categoryBusinessRules;
    public CategoryManager(ICategoryDal categoryDal, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
    {
        _categoryDal = categoryDal;
        _mapper = mapper;
        _categoryBusinessRules = categoryBusinessRules;
    }

    [ValidationAspect(typeof(CreateCategoryRequestValidatior))]
    public async Task<CreatedCategoryResponse> Add(CreateCategoryRequest createCategoryRequest)
    {
       await _categoryBusinessRules.MaximumCategoryIsTen();
        Category category = _mapper.Map<Category>(createCategoryRequest);
        Category createdCategory = await _categoryDal.AddAsync(category);
        CreatedCategoryResponse createdCategoryResponse = _mapper.Map<CreatedCategoryResponse>(createdCategory);
        return createdCategoryResponse;

    }

    public async Task<DeletedCategoryResponse> Delete(DeleteCategoryRequest deleteCategoryRequest)
    {
        Category category = _mapper.Map<Category>(deleteCategoryRequest);
        Category deletedCategory = await _categoryDal.DeleteAsync(category);
        DeletedCategoryResponse deletedCategoryResponse = _mapper.Map<DeletedCategoryResponse>(deletedCategory);
        return deletedCategoryResponse;
    }

    [CacheAspect]
    public async Task<GetListCategoryResponse> GetAsync(Guid id)
    {
        var data = await _categoryDal.GetAsync(predicate: p => p.Id == id);
        GetListCategoryResponse getListCategoryResponse = _mapper.Map<GetListCategoryResponse>(data);
        return getListCategoryResponse;
    }

    [CacheAspect]
    public async Task<IPaginate<GetListCategoryResponse>> GetListAsync(PageRequest pageRequest)
    {
        var data = await _categoryDal.GetListAsync(index: pageRequest.PageIndex,
          size: pageRequest.PageSize);
        var result = _mapper.Map<Paginate<GetListCategoryResponse>>(data);
        return result;
    }

    [ValidationAspect(typeof(UpdateCategoryRequestValidatior))]
    public async Task<UpdatedCategoryResponse> Update(UpdateCategoryRequest updateCategoryRequest)
    {
        Category category = _mapper.Map<Category>(updateCategoryRequest);
        Category updatedCategory = await _categoryDal.DeleteAsync(category);
        UpdatedCategoryResponse updatedCategoryResponse = _mapper.Map<UpdatedCategoryResponse>(updatedCategory);
        return updatedCategoryResponse;
    }
}
