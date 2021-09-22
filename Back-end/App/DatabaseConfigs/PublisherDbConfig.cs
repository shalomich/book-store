
using BookStore.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DatabaseConfigs
{
    public class PublisherDbConfig : RelatedEntityDbConfig<Publisher>
    {
        public override void Configure(EntityTypeBuilder<Publisher> builder)
        {
            base.Configure(builder);
            builder.HasIndex(publisher => publisher.Name).IsUnique();
        }
    }
}
