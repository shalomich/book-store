using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Storage.DatabaseConfigs
{
    public class PublicationDbConfig : IEntityTypeConfiguration<Publication>
    {
        public void Configure(EntityTypeBuilder<Publication> builder)
        {
            builder.ToTable("publications");

            var genresConversion = new ValueConverter<ISet<string>, string>(
                collection => JsonSerializer.Serialize(collection, null),
                str => JsonSerializer.Deserialize<ISet<string>>(str, null));
            builder.Property(publication => publication.Genres).HasConversion(genresConversion);

            builder.Property(publication => publication.Type).IsRequired();
            builder.Property(publication => publication.ISBN).IsRequired();
            builder.Property(publication => publication.AgeLimit).IsRequired();

            builder.HasIndex(publication => publication.ISBN).IsUnique();
            builder.HasIndex(publication =>
                new 
                { 
                    publication.Name, 
                    publication.ReleaseYear, 
                }).IsUnique();
            
      }
    }
}