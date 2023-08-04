using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class BookConfiguration: IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(book => book.Id);

            builder.Property(book => book.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(book => book.Author)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(book => book.Price)
                  .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(book => book.LaunchDate)
                .IsRequired();
        }
    }
}
