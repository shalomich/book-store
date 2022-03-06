
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BookStore.Persistance
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<int>,int>
    {
        public DbSet<Book> Books { set; get; }
        public DbSet<Author> Authors { set; get; }
        public DbSet<Publisher> Publishers { set; get; }
        public DbSet<BookType> BookTypes { set; get; }
        public DbSet<AgeLimit> AgeLimits { set; get; }
        public DbSet<CoverArt> CoverArts { set; get; }
        public DbSet<Genre> Genres { set; get; }
        public DbSet<Tag> Tags { set; get; }
        public DbSet<BasketProduct> BasketProducts { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<Mark> Marks { set; get; }


        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Mark>().ToTable("Marks");
        }
    }
}
