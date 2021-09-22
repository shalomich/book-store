
using BookStore.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookStore.Persistance.DatabaseConfigs
{
    public class ImageDbConfig : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");

            builder.Property(image => image.Name).IsRequired();
            builder.Property(image => image.Format).IsRequired();
            builder.Property(image => image.Data).IsRequired();

            builder.HasIndex(image => new { image.Name, image.AlbumId }).IsUnique();
        }
    }
}
