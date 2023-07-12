using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context
{
    public class ContextConfiguration: IDesignTimeDbContextFactory<PostgreSQLContext>
    {
        private readonly IConfiguration _configuration;

        public ContextConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PostgreSQLContext CreateDbContext(string[] args)
        {
            var connectionString = "";
            var optionsBuilder = new DbContextOptionsBuilder<PostgreSQLContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new PostgreSQLContext(optionsBuilder.Options);
        }
    }
}
