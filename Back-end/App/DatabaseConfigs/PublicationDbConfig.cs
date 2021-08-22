using App.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DatabaseConfigs
{
    public class PublicationDbConfig : ProductDbConfig<Publication>
    {
        public override void Configure(EntityTypeBuilder<Publication> builder)
        {
            base.Configure(builder);

            builder.ToTable("Publications");

            builder.Property(publication => publication.Isbn).IsRequired();
            builder.HasIndex(publication => publication.Isbn).IsUnique();
 
            builder.HasIndex(publication =>
                new
                {
                    publication.Name,
                    publication.ReleaseYear,
                    publication.AuthorId,
                    publication.PublisherId
                }).IsUnique();
        }
    }
}