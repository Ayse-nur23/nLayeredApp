using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation.Middleware;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, IServiceProvider serviceProvider)
    {
        var endpoint = httpContext.GetEndpoint();
        if (endpoint != null)
        {
            var validators = endpoint.Metadata
                .Select(m => serviceProvider.GetService(m as Type))
                .OfType<IValidator>();

            foreach (var validator in validators)
            {
                var validationResult = await validator.ValidateAsync((IValidationContext)httpContext);
                if (!validationResult.IsValid)
                {
                    // Doğrulama hatası durumunda işlem yapın
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await httpContext.Response.WriteAsJsonAsync(validationResult.Errors);
                    return;
                }
            }
        }

        await _next(httpContext);
    }
}