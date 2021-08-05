using Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Storage.DatabaseConfigs;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage
{
    public class ApplicationContext : IdentityDbContext<User,Role,long>
    {
        public DbSet<Publication> Publications { set; get; }
        public DbSet<Author> Authors { set; get; }
        public DbSet<Publisher> Publishers { set; get; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PublicationDbConfig());
            modelBuilder.ApplyConfiguration(new AuthorDbConfig());
            modelBuilder.ApplyConfiguration(new PublisherDbConfig());
            modelBuilder.ApplyConfiguration(new ImageDbConfig());
            modelBuilder.ApplyConfiguration(new EntityDbConfig());
            modelBuilder.ApplyConfiguration(new RoleDbConfig());
        }
    }
}
