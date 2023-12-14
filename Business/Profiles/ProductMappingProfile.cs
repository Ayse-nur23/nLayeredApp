using AutoMapper;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.DataAccess.Dynamic;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, GetListProductResponse>().ForMember(destinationMember: 
            p => p.CategoryName,
            memberOptions: opt => opt.MapFrom(p => p.Category.Name)).ReverseMap();

        CreateMap<Product, CreatedProductResponse>().ReverseMap();
        CreateMap<Product, DeletedProductResponse>().ReverseMap();
        CreateMap<Product, UpdatedProductResponse>().ReverseMap();

        CreateMap<Paginate<Product>, Paginate<GetListProductResponse>>().ReverseMap();

        CreateMap<Product, CreateProductRequest>().ReverseMap();
        CreateMap<Product, DeleteProductRequest>().ReverseMap();
        CreateMap<Product, UpdateProductRequest>().ReverseMap();
    }
}
