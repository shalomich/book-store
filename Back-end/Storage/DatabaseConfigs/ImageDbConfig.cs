using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;
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
            builder.Property(image => image.Format).IsRequired();
            builder.Property(image => image.Encoding).IsRequired();
            builder.Property(image => image.Data).IsRequired();
        }
    }
}
