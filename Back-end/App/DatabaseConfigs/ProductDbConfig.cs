using App.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DatabaseConfigs
{
    public abstract class ProductDbConfig<T> : EntityDbConfig<T> where T : Product
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
        }
    }
}
