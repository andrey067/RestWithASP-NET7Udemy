using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Presentation.Configuration
{
    public static class SwaggerConfiguration
    {
        public static WebApplication ConfigureSwagger(this WebApplication app)
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
