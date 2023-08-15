using Bogus;
using Domain.Entities;
using Domain.Shared.Interfaces;
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
                var cryptography = scope.ServiceProvider.GetRequiredService<ICryptography>();

                if (dbContext.Database.GetPendingMigrations().Any())
                    await dbContext.Database.MigrateAsync();

                if (!dbContext.Persons.Any())
                {
                    var personFaker = new Faker<Domain.Entities.Person>()
                 .CustomInstantiator(f => new Domain.Entities.Person(
                     f.Person.FirstName,
                     f.Person.LastName,
                     f.Address.StreetAddress(),
                     f.PickRandom("Male", "Female")
                 ));

                    var persons = personFaker.Generate(1000);

                    await dbContext.Persons.AddRangeAsync(persons);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Books.Any())
                {
                    var bookFaker = new Faker<Book>()
                 .CustomInstantiator(f => new Book(
                     f.Random.Words(2),
                     f.Person.FullName,
                     f.Random.Decimal(10, 100),
                     f.Date.Past()
                 ));

                    var books = bookFaker.Generate(1000);

                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Users.Any())
                {
                    dbContext.Users.AddRange(
                        new User("john_doe", "John Doe", cryptography.HashPassword("hashed_password_1")),
                                new User("jane_smith", "Jane Smith", cryptography.HashPassword("hashed_password_2")));


                    var userFaker = new Faker<User>().CustomInstantiator(f => new User(
                    f.Internet.UserName(),
                    f.Person.FullName,
                    cryptography.HashPassword(f.Internet.Password())
                ));

                    var users = userFaker.Generate(1000);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
