using Domain.Repository;
using EvolveDb;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Serilog;

namespace Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddScoped<IPersonRepository, PersonRepository>();
            var t = configuration.GetConnectionString("PostgresConnectionString");

            //services.AddDbContext<PostgreSQLContext>(options => options.UseNpgsql(configuration.GetConnectionString("")));
            services.AddDbContext<PostgreSQLContext>(options => options.UseNpgsql("Server=192.168.237.70:5432;DataBase=rest_with_asp_net_udemy;Uid=postgres;Pwd=admin"));

            if (environment.IsDevelopment())
            {
                MigrateDatabase(configuration.GetConnectionString("PostgresConnectionString"));
                MigrateDatabase("Server=192.168.237.70:5432;DataBase=rest_with_asp_net_udemy;Uid=postgres;Pwd=admin");
            }

            return services;
        }

        private static void MigrateDatabase(string connectionString)
        {
            try
            {
                var evolveConnection = new NpgsqlConnection(connectionString);
                var evolve = new Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "Context/Migrations", "Context/Dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }
    }
}
