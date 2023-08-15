using Domain.Entities;
using Domain.Repository;
using Domain.Shared.Interfaces;
using Infrastructure.Configuration;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            //services.AddDbContext<RestFullContext>(options =>
            //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<RestFullContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ICryptography, Cryptography>();
            services.AddScoped<IUserRepository, UserRepository>();

            TypeAdapterConfig.GlobalSettings.Scan(assembly);
            return services;
        }
    }
}
