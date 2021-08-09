using App.Entities.Publications;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DatabaseConfigs
{
    public class GenreDbConfig : EntityDbConfig<Genre>
    {
        public override void Configure(EntityTypeBuilder<Genre> builder)
        {
            base.Configure(builder);

            builder.HasIndex(type => type.Name).IsUnique();

            builder.HasData(new Genre[]
            {
                new Genre{Id = 1, Name = "Драма"},
                new Genre{Id = 2, Name = "Ужасы"},
                new Genre{Id = 3, Name = "Научная фантастика"},
                new Genre{Id = 4, Name = "Наука"},
                new Genre{Id = 5, Name = "Боевик"},
                new Genre{Id = 6, Name = "Детектив"},
                new Genre{Id = 7, Name = "Фэнтези"}
            });
        }
    }
}
