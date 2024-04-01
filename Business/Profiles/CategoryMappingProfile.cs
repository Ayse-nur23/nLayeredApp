using AutoMapper;
using Business.Dtos.Categories;
using Core.DataAccess.Dynamic;
using Entities.Concrete;

namespace Business.Profiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CreatedCategoryResponse>().ReverseMap();
        CreateMap<Category, DeletedCategoryResponse>().ReverseMap();
        CreateMap<Category, UpdatedCategoryResponse>().ReverseMap();



        CreateMap<Category, CreateCategoryRequest>().ReverseMap();
        CreateMap<Category, DeleteCategoryRequest>().ReverseMap();
        CreateMap<Category, UpdateCategoryRequest>().ReverseMap();

        CreateMap<Paginate<Category>, Paginate<GetListCategoryResponse>>().ReverseMap();
        CreateMap<Category, GetListCategoryResponse>().ReverseMap();
    }
}
