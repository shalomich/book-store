using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
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
            int idCounter = 0;

            var roles = Enum.GetValues<UserRole>()
                .Select(role => new Role
                {
                    Id = ++idCounter,
                    Name = role.ToString(),
                    NormalizedName = role.ToString().ToUpper()
                })
                .ToArray();
            
            builder.HasData(roles);
        }
    }
}
