using App.Entities.Publications;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DatabaseConfigs
{
    public class PublicationTypeDbConfig : EntityDbConfig<PublicationType>
    {
        public override void Configure(EntityTypeBuilder<PublicationType> builder)
        {
            base.Configure(builder);

            builder.HasIndex(type => type.Name).IsUnique();

            builder.HasData(new PublicationType[]
            {
                new PublicationType {Id = 1, Name = "Книга"},
                new PublicationType {Id = 2, Name = "Манга"},
                new PublicationType {Id = 3, Name = "Ранобэ"},
                new PublicationType {Id = 4, Name = "Графический роман"},
                new PublicationType {Id = 5, Name = "Артбук"}
            });
        }
    }
}
