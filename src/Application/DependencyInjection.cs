using Application.Configuration;
using Application.Interfaces;
using Application.Services;
using Domain.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILoginServices, LoginServices>();


            return services;
        }
    }
}
