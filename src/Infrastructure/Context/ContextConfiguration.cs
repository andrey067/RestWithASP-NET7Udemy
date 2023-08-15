using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Context
{
    public class ContextConfiguration: IDesignTimeDbContextFactory<RestFullContext>
    {
        public RestFullContext CreateDbContext(string[] args)
        {
            //var connectionString = "Server=192.168.237.70:5432;DataBase=rest_with_asp_net_udemy;Uid=postgres;Pwd=admin";
            var connectionString = "Host=dpg-cjdt8qbbq8nc73ce4vk0-a.oregon-postgres.render.com:5432; Database=rest_with_asp_net_udemy; Username=andrey_067; Password=OI0tg1iRLJt9KvNqCazXuJMDD6KFmwhm";
            var optionsBuilder = new DbContextOptionsBuilder<RestFullContext>();
            //optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseNpgsql(connectionString);
            return new RestFullContext(optionsBuilder.Options);
        }
    }
}
