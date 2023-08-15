using Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Presentation.Configuration
{
    public static class JwtConfigurations
    {
        public static IServiceCollection AddJwtConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfigurationOptions = new TokenConfigurationOptions();
            configuration.GetSection(TokenConfigurationOptions.TokenConfiguration).Bind(tokenConfigurationOptions);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurationOptions.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    //ValidIssuer = configuration.GetValue<string>("TokenConfigurationOptions:ValidIssuer"),
                    //ValidAudience = configuration.GetValue<string>("TokenConfigurationOptions:ValidAudience"),
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                                           .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                           .RequireAuthenticatedUser().Build());

            });

            return services;
        }
    }
}
