
using App.DatabaseConfigs;
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QueryWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class ApplicationContext : IdentityDbContext<User,Role,int>
    {
        public DbSet<Book> Books { set; get; }
        public DbSet<Author> Authors { set; get; }
        public DbSet<Publisher> Publishers { set; get; }
        public DbSet<BookType> BookTypes { set; get; }
        public DbSet<AgeLimit> AgeLimits { set; get; }
        public DbSet<CoverArt> CoverArts { set; get; }
        public DbSet<Genre> Genres { set; get; }
        public DbSet<Basket> Baskets { set; get; }
        public DbSet<BasketProduct> BasketProducts { set; get; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<Product>().ToTable("Products");

            modelBuilder.Entity<User>()
                .HasOne(user => user.Basket)
                .WithOne(basket => basket.User)
                .HasForeignKey<Basket>(basket => basket.UserId);
        }
    }
}
