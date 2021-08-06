using App.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.DatabaseConfigs
{
    public abstract class ProductDbConfig<T> : IEntityTypeConfiguration<T> where T : Product
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
        }
    }
}
