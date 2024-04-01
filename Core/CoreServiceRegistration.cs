using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.FileUpload;
using Core.Utilities.IoC;
using Core.Utilities.Security.JWT;
using Core.Utilities.Verification.TCKN;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core;

public static class CoreServiceRegistration
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {

        services.AddMemoryCache();
        services.AddScoped<ICacheManager, MemoryCacheManager>();

        services.AddScoped<Stopwatch>();

        services.AddScoped<ITokenHelper, JwtHelper>();
        services.AddScoped<IFileUploadAdapter, CloudinaryAdapter>();
        services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IVerificationService, TCKNVerificationService>();

        ServiceTool.Create(services);

        return services;
    }
}