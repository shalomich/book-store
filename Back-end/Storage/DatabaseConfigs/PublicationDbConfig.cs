﻿using Microsoft.EntityFrameworkCore;
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
    public class PublicationDbConfig : ProductDbConfig<Publication>
    {
        public override void Configure(EntityTypeBuilder<Publication> builder)
        {
            base.Configure(builder);

            builder.ToTable("publications");

            var genresConversion = new ValueConverter<ISet<string>, string>(
                collection => JsonSerializer.Serialize(collection, null),
                str => JsonSerializer.Deserialize<ISet<string>>(str, null));
            builder.Property(publication => publication.Genres).HasConversion(genresConversion);

            builder.Property(publication => publication.Type).IsRequired();
            builder.Property(publication => publication.Isbn).IsRequired();
            builder.Property(publication => publication.AgeLimit).IsRequired();

            builder.HasIndex(publication => publication.Isbn).IsUnique();
            builder.HasIndex(publication =>
                new
                {
                    publication.Name,
                    publication.ReleaseYear,
                    publication.AuthorId,
                    publication.PublisherId
                }).IsUnique();
        }
    }
}