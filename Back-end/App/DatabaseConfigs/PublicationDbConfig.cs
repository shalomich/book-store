﻿using App.Entities;
using App.Entities.Publications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.DatabaseConfigs
{
    public class PublicationDbConfig : ProductDbConfig<Publication>
    {
        public override void Configure(EntityTypeBuilder<Publication> builder)
        {
            base.Configure(builder);

            builder.ToTable("Publications");

            builder.Property(publication => publication.Isbn).IsRequired();
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