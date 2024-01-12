using Business.Abstract;
using Business.Concrete;
using Business.Concretes;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Business.Rules;
using Core.Business.Validation;
using Core.Utilities.Security.JWT;
using FluentValidation;
using MediatR;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));


        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<ICategoryService, CategoryManager>();
        services.AddScoped<ICustomerService, CustomerManager>();
        services.AddScoped<ICustomerCustomerDemoService, CustomerCustomerDemoManager>();

        services.AddScoped<IUserService, UserManager>();
        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IUserOperationClaimService, UserOperationClaimManager>();
        services.AddScoped<ITokenHelper, JwtHelper>();


        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<, >), typeof(RequestValidationBehavior<, >));

        return services;
    }

    public static IServiceCollection AddSubClassesOfType(this IServiceCollection services, Assembly assembly, Type type,
                            Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null)
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);

            else
                addWithLifeCycle(services, type);
        return services;
    }

}
