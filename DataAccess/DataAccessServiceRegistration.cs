using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DataAccess;

public static class DataAccessServiceRegistration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NorthwindCloneContext>(options => options.UseSqlServer(configuration.GetConnectionString("NorthwindClone")).EnableSensitiveDataLogging());

        services.AddScoped<IProductDal, EfProductDal>();
        services.AddScoped<ICategoryDal, EfCategoryDal>();
        services.AddScoped<ICustomerDal, EfCustomerDal>();
        services.AddScoped<IRefreshTokenDal, EfRefreshTokenDal>();
        services.AddScoped<IFileUploadDal, EfFileUploadDal>();
        services.AddSingleton<IUserDal, EfUserDal>();
        services.AddScoped<IUserOperationClaimDal, EfUserOperationClaimDal>();

        return services;
    }
}
