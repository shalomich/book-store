using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DatabaseConfigs
{
    public abstract class ProductDbConfig<T> : IEntityTypeConfiguration<T> where T : Product
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(entity => entity.Name).IsRequired();
        }
    }
}
