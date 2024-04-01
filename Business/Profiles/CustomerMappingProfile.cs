using AutoMapper;
using Business.Dtos.Customers;
using Core.DataAccess.Dynamic;
using Entities.Concrete;

namespace Business.Profiles;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, CreatedCustomerResponse>().ReverseMap();
        CreateMap<Customer, UpdatedCustomerResponse>().ReverseMap();
        CreateMap<Customer, DeletedCustomerResponse>().ReverseMap();



        CreateMap<Customer, CreateCustomerRequest>().ReverseMap();
        CreateMap<Customer, DeleteCustomerRequest>().ReverseMap();
        CreateMap<Customer, UpdateCustomerRequest>().ReverseMap();


        CreateMap<Paginate<Customer>, Paginate<GetListCustomerResponse>>().ReverseMap();
        CreateMap<Customer, GetListCustomerResponse>().ReverseMap();
       
    }
}