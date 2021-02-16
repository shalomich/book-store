using Microsoft.EntityFrameworkCore;
using Storage.DatabaseConfigs;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage
{
    public class Database : DbContext
    {
        public DbSet<Publication> Publications { set; get; }

        public Database(DbContextOptions<Database> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PublicationDbConfig());
        }
    }
}
