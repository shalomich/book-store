using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistance.DatabaseConfigs
{
    public class OrderDbConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(order => order.Email).IsRequired();
            builder.Property(order => order.UserName).IsRequired();
            builder.Property(order => order.PhoneNumber).IsRequired();
        }
    }
}
