using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Builder;
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
            services.AddJwtConfigurations(configuration);
            return services;
        }

        public static IEndpointRouteBuilder ConfigureEndpoints(this IEndpointRouteBuilder routers)
        {
            routers.MapGroup("/api/user")
                   .AllowAnonymous()
                   .MapUserController()
                   .MapSwagger();

            routers.MapGroup("/api/person").RequireAuthorization("Bearer")
                                           .MapPersonController()
                                           .MapSwagger();

            return routers;
        }
    }
}
