using App.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DatabaseConfigs
{
    public class BookDbConfig : ProductDbConfig<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            base.Configure(builder);

            builder.ToTable("Books");

            builder.Property(book => book.Isbn).IsRequired();
            builder.HasIndex(book => book.Isbn).IsUnique();
 
            builder.HasIndex(book =>
                new
                {
                    book.Name,
                    book.ReleaseYear,
                    book.AuthorId,
                    book.PublisherId
                }).IsUnique();
        }
    }
}