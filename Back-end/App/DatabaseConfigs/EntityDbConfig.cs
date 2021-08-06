using App.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.DatabaseConfigs
{
    public class EntityDbConfig : IEntityTypeConfiguration<Entity> 
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.ToTable("entities");
            builder.Property(entity => entity.Name).IsRequired();
            builder.Property(entity => entity.TitleImageName).IsRequired();
        }
    }
}
