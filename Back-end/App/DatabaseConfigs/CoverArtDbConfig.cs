using App.Entities.Publications;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DatabaseConfigs
{
    public class CoverArtDbConfig : EntityDbConfig<CoverArt>
    {
        public override void Configure(EntityTypeBuilder<CoverArt> builder)
        {
            base.Configure(builder);

            builder.HasIndex(type => type.Name).IsUnique();

            builder.HasData(new PublicationType[]
            {
                new PublicationType {Id = 1, Name = "Мягкая"},
                new PublicationType {Id = 2, Name = "Твердая"}
            });
        }
    }
}
