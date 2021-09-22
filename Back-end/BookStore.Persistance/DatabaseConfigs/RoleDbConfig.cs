using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Persistance.DatabaseConfigs
{
    public class RoleDbConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role { Id = 1, Name = "admin", NormalizedName = "ADMIN"},
                new Role { Id = 2, Name = "customer", NormalizedName = "CUSTOMER"}
            );
        }
    }
}
