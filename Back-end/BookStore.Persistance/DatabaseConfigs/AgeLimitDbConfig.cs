using BookStore.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Persistance.DatabaseConfigs
{
    public class AgeLimitDbConfig : RelatedEntityDbConfig<AgeLimit>
    {
        public override void Configure(EntityTypeBuilder<AgeLimit> builder)
        {
            base.Configure(builder);

            builder.HasIndex(type => type.Name).IsUnique();

            builder.HasData(new AgeLimit[]
            {
                new AgeLimit {Id = 1, Name = "0+"},
                new AgeLimit {Id = 2, Name = "6+"},
                new AgeLimit {Id = 3, Name = "12+"},
                new AgeLimit {Id = 4, Name = "18+"}
            });
        }
    }
}
