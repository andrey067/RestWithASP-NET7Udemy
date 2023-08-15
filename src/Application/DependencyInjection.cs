using Application.Configuration;
using Application.Interfaces;
using Application.Services;
using Domain.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<ILoginServices, LoginServices>();
            services.AddScoped<IPersonBusiness, PersonBusiness>();
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
