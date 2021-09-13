using App.Entities.Books;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DatabaseConfigs
{
    public class CoverArtDbConfig : RelatedEntityDbConfig<CoverArt>
    {
        public override void Configure(EntityTypeBuilder<CoverArt> builder)
        {
            base.Configure(builder);

            builder.HasIndex(type => type.Name).IsUnique();

            builder.HasData(new BookType[]
            {
                new BookType {Id = 1, Name = "Мягкая"},
                new BookType {Id = 2, Name = "Твердая"}
            });
        }
    }
}
