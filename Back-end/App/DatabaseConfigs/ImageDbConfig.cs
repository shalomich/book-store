using App.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.DatabaseConfigs
{
    public class ImageDbConfig : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("images");

            builder.Property(image => image.Name).IsRequired();
            builder.Property(image => image.Format).IsRequired();
            builder.Property(image => image.Encoding).IsRequired();
            builder.Property(image => image.Data).IsRequired();
        }
    }
}
