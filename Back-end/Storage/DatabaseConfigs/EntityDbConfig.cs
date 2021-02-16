using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.DatabaseConfigs
{
    abstract public class EntityDbConfig<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(entity => entity.TitleImageName).IsRequired();
            builder.Property(entity => entity.Images).IsRequired();
        }
    }
}
