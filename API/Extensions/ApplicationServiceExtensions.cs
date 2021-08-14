using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
             services.AddScoped<IProductRepository,ProductRepository>();
            // Adding Generic Repository to the service (Important as well as new)
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Adding BasketRepository to the serivce
            services.AddScoped<IBasketRepository,BasketRepository>();
            
            // Create an instance when we use it Dependency Injection
            services.AddScoped<ITokenService,TokenService>();
            
            services.Configure<ApiBehaviorOptions>( options =>
            {
                options.InvalidModelStateResponseFactory =  actionContext => {
                    var errors = actionContext.ModelState
                                    .Where(e => e.Value.Errors.Count > 0)
                                    .SelectMany(err => err.Value.Errors)
                                    .Select(errMsg => errMsg.ErrorMessage ).ToArray();
                    
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);

                };
            });

            return services;
        }
    }
}