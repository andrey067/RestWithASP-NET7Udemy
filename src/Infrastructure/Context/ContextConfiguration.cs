using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Context
{
    public class ContextConfiguration: IDesignTimeDbContextFactory<RestFullContext>
    {
        public RestFullContext CreateDbContext(string[] args)
        {
            //var connectionString = "Server=192.168.237.70:5432;DataBase=rest_with_asp_net_udemy;Uid=postgres;Pwd=admin";
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=rest_with_asp_net_udemy;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            var optionsBuilder = new DbContextOptionsBuilder<RestFullContext>();
            //optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseSqlServer(connectionString);
            return new RestFullContext(optionsBuilder.Options);
        }
    }
}
