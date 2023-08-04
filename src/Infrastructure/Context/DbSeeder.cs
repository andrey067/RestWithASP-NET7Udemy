using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Context
{
    public static class DbSeeder
    {
        public static async Task AplicarMigracoes(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<RestFullContext>();

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    await dbContext.Database.MigrateAsync();
                }

                if (!dbContext.Persons.Any())
                {
                    await dbContext.Persons.AddRangeAsync(
                          new Person("John", "Doe", "123 Main St", "Male"),
                          new Person("Jane", "Smith", "456 Oak Ave", "Female"),
                          new Person("Michael", "Johnson", "789 Elm Rd", "Male"),
                          new Person("Emily", "Brown", "567 Pine Dr", "Female"));

                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Books.Any())
                {
                    await dbContext.Books.AddRangeAsync(
                          new Book("Book 1", "Author 1", 19.99m, new DateTime(2023, 7, 20)),
                          new Book("Book 2", "Author 2", 29.99m, new DateTime(2023, 7, 21)),
                          new Book("Book 3", "Author 3", 14.99m, new DateTime(2023, 7, 22)),
                          new Book("Book 4", "Author 4", 9.99m, new DateTime(2023, 7, 23)));

                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
