using Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Configuration
{
    public static class ConfigureOptions
    {
        public static IServiceCollection AddOptionsConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenConfigurationOptions>(configuration.GetSection(TokenConfigurationOptions.TokenConfiguration));
            return services;
        }
    }
}
