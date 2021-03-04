using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.DatabaseConfigs
{
    public abstract class ProductDbConfig<T> : EntityDbConfig<T> where T : Product
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
        }
    }
}
