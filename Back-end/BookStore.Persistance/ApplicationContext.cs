
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Battles;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Persistance;
public class ApplicationContext : IdentityDbContext<User, IdentityRole<int>,int>
{
    public DbSet<Book> Books { set; get; }

    #region Book related entities
    public DbSet<Author> Authors { set; get; }
    public DbSet<AuthorSelectionOrder> AuthorSelectionOrders { set; get; }
    public DbSet<Publisher> Publishers { set; get; }
    public DbSet<BookType> BookTypes { set; get; }
    public DbSet<AgeLimit> AgeLimits { set; get; }
    public DbSet<CoverArt> CoverArts { set; get; }
    public DbSet<Genre> Genres { set; get; }
    public DbSet<GenreBook> BookGenreLinks { set; get; }
    public DbSet<Tag> TagGroups { set; get; }
    public DbSet<Tag> Tags { set; get; }
    public DbSet<ProductTag> BookTagLinks { set; get; }
        
    #endregion

    #region Product related entities
    public DbSet<Album> Albums { set; get; }
    public DbSet<Image> Images { set; get; }
    public DbSet<Discount> Discounts { set; get; }
    public DbSet<ProductCloseout> Closeouts { set; get; }
    public DbSet<Mark> Marks { set; get; }
    public DbSet<View> Views { set; get; }

    #endregion

    #region User
    public DbSet<TelegramBotContact> TelegramBotContacts { set; get; }

    #endregion

    #region Sale
    public DbSet<BasketProduct> BasketProducts { set; get; }
    public DbSet<Order> Orders { set; get; }
    public DbSet<OrderProduct> OrderProducts { set; get; }

    #endregion

    #region Battle

    public DbSet<Battle> Battles { set; get; }
    public DbSet<BattleBook> BattleBookLinks { set; get; }
    public DbSet<Vote> Votes { set; get; }

    #endregion

    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        SetUpEntity(modelBuilder.Entity<User>());

        SetUpEntity(modelBuilder.Entity<Product>());
        SetUpEntity(modelBuilder.Entity<Book>());

        SetUpBookRelatedEntity(modelBuilder.Entity<Author>());
        SetUpBookRelatedEntity(modelBuilder.Entity<Publisher>());
        SetUpBookRelatedEntity(modelBuilder.Entity<Genre>());
        SetUpBookRelatedEntity(modelBuilder.Entity<BookType>());
        SetUpBookRelatedEntity(modelBuilder.Entity<AgeLimit>());
        SetUpBookRelatedEntity(modelBuilder.Entity<CoverArt>());
        SetUpEntity(modelBuilder.Entity<TagGroup>());
        SetUpEntity(modelBuilder.Entity<Tag>());

        SetUpEntity(modelBuilder.Entity<Album>());
        SetUpEntity(modelBuilder.Entity<Image>());

        SetUpEntity(modelBuilder.Entity<Order>());
    }

    private void SetUpEntity(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.FirstName).IsRequired();

        builder.HasMany(user => user.Tags)
            .WithMany(tag => tag.Users)
            .UsingEntity(userTag => userTag.ToTable("UserTagLinks"));
    }

    private void SetUpEntity(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.Property(entity => entity.Name).IsRequired();
    }

    private void SetUpEntity(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.Property(book => book.Isbn).IsRequired();
        builder.HasIndex(book => book.Isbn).IsUnique();

        builder.HasIndex(book => new
        {
            book.Name,
            book.ReleaseYear,
            book.AuthorId,
            book.PublisherId
        }).IsUnique();
    }

    private void SetUpBookRelatedEntity<T>(EntityTypeBuilder<T> builder) where T : RelatedEntity
    {
        builder.Property(relatedEntity => relatedEntity.Name).IsRequired();
        builder.HasIndex(relatedEntity => relatedEntity.Name).IsUnique();
    }

    private void SetUpEntity(EntityTypeBuilder<TagGroup> builder)
    {
        builder.ToTable("TagGroups");

        SetUpBookRelatedEntity(builder);
    }

    private void SetUpEntity(EntityTypeBuilder<Tag> builder)    
    {
        builder.ToTable("Tags");

        SetUpBookRelatedEntity(builder);
    }

    private void SetUpEntity(EntityTypeBuilder<Album> builder)
    {
        builder.Ignore(album => album.TitleImage);
        builder.Ignore(album => album.NotTitleImages);
    }

    private void SetUpEntity(EntityTypeBuilder<Image> builder)
    {
        builder.Property(image => image.Name).IsRequired();
        builder.Property(image => image.Format).IsRequired();
        builder.Property(image => image.Data).IsRequired();

        builder.HasIndex(image => new { image.Name, image.AlbumId }).IsUnique();
    }

    private void SetUpEntity(EntityTypeBuilder<Order> builder)
    {
        builder.Property(order => order.Email).IsRequired();
        builder.Property(order => order.UserName).IsRequired();
        builder.Property(order => order.PhoneNumber).IsRequired();
    }
}
