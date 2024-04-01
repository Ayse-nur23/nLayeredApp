using AutoMapper;
using Business.Dtos.Products;
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
        CreateMap<Paginate<Product>, Paginate<GetListProductResponse>>().ReverseMap();
        CreateMap<Product, GetListProductResponse>().ReverseMap();


        CreateMap<Product, CreatedProductResponse>().ReverseMap();
        CreateMap<Product, CreateProductRequest>().ReverseMap();

        CreateMap<Product, DeleteProductRequest>().ReverseMap();
        CreateMap<Product, DeletedProductResponse>().ReverseMap();

        CreateMap<Product, UpdateProductRequest>().ReverseMap();
        CreateMap<Product, UpdatedProductResponse>().ReverseMap();

    }
}
