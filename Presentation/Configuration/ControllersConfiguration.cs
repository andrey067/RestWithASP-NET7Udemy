using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Controllers;

namespace Presentation.Configuration
{
    public static class ControllersConfiguration
    {
        public static IEndpointRouteBuilder ConfigureEndpoints(this WebApplication app)
        {
            var versionSet = ConfigureEndpointVersion(app);

            app.MapGroup($"/api/v{versionSet}/person").WithApiVersionSet(versionSet).MapPersonController();

            return app;
        }

        public static IServiceCollection ConfigureApiVersion(this IServiceCollection service)
        {
            service.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            return service;
        }

        private static ApiVersionSet ConfigureEndpointVersion(WebApplication app)
        => app.NewApiVersionSet()
                  .HasApiVersion(new ApiVersion(1, 0))
                  .HasApiVersion(new ApiVersion(2, 0))
               .ReportApiVersions()
               .Build();
    }
}
