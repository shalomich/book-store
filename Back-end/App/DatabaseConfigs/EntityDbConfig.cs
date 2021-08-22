using App.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DatabaseConfigs
{
    public class EntityDbConfig<T> : IEntityTypeConfiguration<T> where T : FormEntity 
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(entity => entity.Name).IsRequired();
        }
    }
}
