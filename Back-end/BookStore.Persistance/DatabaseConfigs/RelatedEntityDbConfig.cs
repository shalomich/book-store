using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Persistance.DatabaseConfigs
{
    public class RelatedEntityDbConfig<T> : IEntityTypeConfiguration<T> where T : RelatedEntity 
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(entity => entity.Name).IsRequired();
        }
    }
}
