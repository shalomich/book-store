using App.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DatabaseConfigs
{
    public class AlbumDbConfig : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Albums");

            builder.Property(album => album.TitleImageName).IsRequired();
            
            builder.Ignore(album => album.TitleImage);
            builder.Ignore(album => album.NotTitleImages);
        }
    }
}
