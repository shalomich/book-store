﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.DatabaseConfigs
{
    public class PublisherDbConfig : EntityDbConfig<Publisher>
    {
        public override void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("publishers");

            builder.Property(publisher => publisher.Name).IsRequired();

            builder.HasIndex(publisher => publisher.Name).IsUnique();
        }
    }
}
