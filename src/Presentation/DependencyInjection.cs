using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Configuration;
using Presentation.Controllers;

namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
            services.AddScoped<IPersonBusiness, PersonBusiness>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDocsConfiguration();
                options.SwaggerSecurityConfiguration();
            });

            services.AddOptionsConfigurations(configuration);
            services.AddJwtConfigurations(configuration);

            return services;
        }

        public static IEndpointRouteBuilder ConfigureEndpoints(this IEndpointRouteBuilder routers)
        {
            routers.MapGroup("/api/user")
                   .MapUserController()
                   .MapSwagger();

            routers.MapGroup("/api/person")
                   .RequireAuthorization()
                   .MapPersonController()
                   .MapSwagger();

            routers.MapGroup("/api/file")
                   .MapFileController();

            return routers;
        }
    }
}
