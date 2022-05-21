
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Persistance.DatabaseConfigs
{
    public class TagGroupDbConfig : RelatedEntityDbConfig<TagGroup>
    {
        public override void Configure(EntityTypeBuilder<TagGroup> builder)
        {
            base.Configure(builder);
            builder.HasIndex(tagGroup => tagGroup.Name).IsUnique();
        }
    }
}
