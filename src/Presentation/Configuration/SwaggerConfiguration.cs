using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Presentation.Configuration
{
    public static class SwaggerConfiguration
    {
        public static SwaggerGenOptions SwaggerDocsConfiguration(this SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "Curso API RestFull", Version = "v1" });
            return swaggerGenOptions;
        }

        public static SwaggerGenOptions SwaggerSecurityConfiguration(this SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Por favor utilize Bearer <TOKEN>",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
                });

            return swaggerGenOptions;
        }

        public static WebApplication ConfigureSwaggerJson(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso API RestFull");
                    c.RoutePrefix = "swagger";
                });
            }

            return app;
        }
    }
}
