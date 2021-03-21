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
        public DbSet<Author> Authors { set; get; }
        public DbSet<Publisher> Publishers { set; get; }

        public Database(DbContextOptions<Database> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PublicationDbConfig());
            modelBuilder.ApplyConfiguration(new AuthorDbConfig());
            modelBuilder.ApplyConfiguration(new PublisherDbConfig());
            modelBuilder.ApplyConfiguration(new ImageDbConfig());
            modelBuilder.ApplyConfiguration(new EntityDbConfig());
        }
    }
}
