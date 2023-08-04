using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class PersonConfiguration: IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(person => person.Id);

            builder.Property(person => person.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(person => person.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(person => person.Address)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(person => person.Gender)
                .HasMaxLength(10)
                .IsRequired();
        }
    }
}
