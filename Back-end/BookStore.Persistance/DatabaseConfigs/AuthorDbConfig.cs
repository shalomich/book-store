
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Persistance.DatabaseConfigs
{
    public class AuthorDbConfig : RelatedEntityDbConfig<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);
            builder.HasIndex(author => author.Name).IsUnique();
        }
    }
}
