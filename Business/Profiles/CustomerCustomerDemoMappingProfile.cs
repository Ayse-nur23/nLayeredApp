using AutoMapper;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.DataAccess.Dynamic;
using Entities.Concrete;

namespace Business.Profiles;

public class CustomerCustomerDemoMappingProfile : Profile
{
    public CustomerCustomerDemoMappingProfile()
    {
        CreateMap<CustomerCustomerDemo, CreatedCustomerCustomerDemoResponse>().ForMember(destinationMember: p => p.CustomerTypeID,
            memberOptions: opt => opt.MapFrom(p => p.CustomerDemographic.Id)).
            ForMember(destinationMember: p => p.CustomerID,
            memberOptions: opt => opt.MapFrom(p => p.Customer.Id))
            .ForMember(destinationMember: p => p.CompanyName,
            memberOptions: opt => opt.MapFrom(p => p.Customer.CompanyName)).
            ForMember(destinationMember: p => p.ContactName,
            memberOptions: opt => opt.MapFrom(p => p.Customer.ContactName)).
            ForMember(destinationMember: p => p.City,
            memberOptions: opt => opt.MapFrom(p => p.Customer.City)).
            ForMember(destinationMember: p => p.Country,
            memberOptions: opt => opt.MapFrom(p => p.Customer.Country)).
            ForMember(destinationMember: p => p.CustomerDesc,
            memberOptions: opt => opt.MapFrom(p => p.CustomerDemographic.CustomerDesc)).ReverseMap();
        CreateMap<CustomerCustomerDemo, DeletedCustomerCustomerDemoResponse>().ReverseMap();
        CreateMap<CustomerCustomerDemo, UpdatedCustomerCustomerDemoResponse>().ReverseMap();


    CreateMap<CustomerCustomerDemo, CreateCustomerCustomerDemoRequest>().ForMember(destinationMember:p => p.CompanyName, 
            memberOptions: opt => opt.MapFrom(p => p.Customer.CompanyName)).
            ForMember(destinationMember: p => p.ContactName,
            memberOptions: opt => opt.MapFrom(p => p.Customer.ContactName)).
            ForMember(destinationMember: p => p.City,
            memberOptions: opt => opt.MapFrom(p => p.Customer.City)).
            ForMember(destinationMember: p => p.Country,
            memberOptions: opt => opt.MapFrom(p => p.Customer.Country)).
            ForMember(destinationMember: p => p.CustomerDesc,
            memberOptions: opt => opt.MapFrom(p => p.CustomerDemographic.CustomerDesc)).
            ReverseMap();
        CreateMap<CustomerCustomerDemo, DeleteCustomerCustomerDemoRequest>().ReverseMap();
        CreateMap<CustomerCustomerDemo, UpdateCustomerCustomerDemoRequest>().ReverseMap();

        CreateMap<Paginate<CustomerCustomerDemo>, Paginate<GetListCustomerCustomerDemoResponse>>().ReverseMap();
        CreateMap<CustomerCustomerDemo, GetListCustomerCustomerDemoResponse>().ForMember(destinationMember: p => p.CompanyName,
            memberOptions: opt => opt.MapFrom(p => p.Customer.CompanyName)).
            ForMember(destinationMember: p => p.ContactName,
            memberOptions: opt => opt.MapFrom(p => p.Customer.ContactName)).
            ForMember(destinationMember: p => p.City,
            memberOptions: opt => opt.MapFrom(p => p.Customer.City)).
            ForMember(destinationMember: p => p.Country,
            memberOptions: opt => opt.MapFrom(p => p.Customer.Country)).
            ForMember(destinationMember: p => p.CustomerDesc,
            memberOptions: opt => opt.MapFrom(p => p.CustomerDemographic.CustomerDesc)).ReverseMap();
    }
}