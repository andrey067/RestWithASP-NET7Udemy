using Domain.Entities;
using Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class RestFullContext: DbContext
    {
        public RestFullContext() { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }

        public RestFullContext(DbContextOptions<RestFullContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
        }
    }
}
