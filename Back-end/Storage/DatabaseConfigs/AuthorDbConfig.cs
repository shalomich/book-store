using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.DatabaseConfigs
{
    public class AuthorDbConfig : EntityDbConfig<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("authors");

            builder.Property(author => author.Name).IsRequired();
            builder.Property(author => author.Surname).IsRequired();
            
            builder.HasIndex(author => new { author.Name, author.Surname, author.BirthDate }).IsUnique();
        }
    }
}
