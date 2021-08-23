﻿// <auto-generated />
using System;
using App;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210823122303_AddBasketProductUniqueIndex")]
    partial class AddBasketProductUniqueIndex
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("App.Entities.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("App.Entities.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("App.Entities.BasketProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BasketId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("BasketId", "ProductId")
                        .IsUnique();

                    b.ToTable("BasketProducts");
                });

            modelBuilder.Entity("App.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("App.Entities.Products.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("TitleImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("App.Entities.Publications.AgeLimit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AgeLimits");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "0+"
                        },
                        new
                        {
                            Id = 2,
                            Name = "6+"
                        },
                        new
                        {
                            Id = 3,
                            Name = "12+"
                        },
                        new
                        {
                            Id = 4,
                            Name = "18+"
                        });
                });

            modelBuilder.Entity("App.Entities.Publications.CoverArt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("CoverArts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Мягкая"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Твердая"
                        });
                });

            modelBuilder.Entity("App.Entities.Publications.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Драма"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ужасы"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Научная фантастика"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Наука"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Боевик"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Детектив"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Фэнтези"
                        });
                });

            modelBuilder.Entity("App.Entities.Publications.GenrePublication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("PublicationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("PublicationId");

                    b.ToTable("GenrePublication");
                });

            modelBuilder.Entity("App.Entities.Publications.PublicationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PublicationTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Книга"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Манга"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Ранобэ"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Графический роман"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Артбук"
                        });
                });

            modelBuilder.Entity("App.Entities.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("App.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "70e7f8d5-bd14-443b-8f21-13c83adc1815",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "a59b9e24-0ce5-4e75-a871-26665dbed366",
                            Name = "customer",
                            NormalizedName = "CUSTOMER"
                        });
                });

            modelBuilder.Entity("App.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("BasketId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("App.Products.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("Name", "AlbumId")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("App.Entities.Publication", b =>
                {
                    b.HasBaseType("App.Entities.Product");

                    b.Property<int?>("AgeLimitId")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int?>("CoverArtId")
                        .HasColumnType("int");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OriginalName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PageQuantity")
                        .HasColumnType("int");

                    b.Property<string>("PublicationFormat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PublisherId")
                        .HasColumnType("int");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasIndex("AgeLimitId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CoverArtId");

                    b.HasIndex("Isbn")
                        .IsUnique()
                        .HasFilter("[Isbn] IS NOT NULL");

                    b.HasIndex("PublisherId");

                    b.HasIndex("TypeId");

                    b.HasIndex("Name", "ReleaseYear", "AuthorId", "PublisherId")
                        .IsUnique();

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("App.Entities.Basket", b =>
                {
                    b.HasOne("App.Entities.User", "User")
                        .WithOne("Basket")
                        .HasForeignKey("App.Entities.Basket", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("App.Entities.BasketProduct", b =>
                {
                    b.HasOne("App.Entities.Basket", "Basket")
                        .WithMany("BasketProducts")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Entities.Product", "Product")
                        .WithMany("BasketProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("App.Entities.Products.Album", b =>
                {
                    b.HasOne("App.Entities.Product", "Product")
                        .WithOne("Album")
                        .HasForeignKey("App.Entities.Products.Album", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("App.Entities.Publications.GenrePublication", b =>
                {
                    b.HasOne("App.Entities.Publications.Genre", "Genre")
                        .WithMany("Publications")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Entities.Publication", "Publication")
                        .WithMany("GenresPublications")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Publication");
                });

            modelBuilder.Entity("App.Products.Entities.Image", b =>
                {
                    b.HasOne("App.Entities.Products.Album", "Album")
                        .WithMany("Images")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("App.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("App.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("App.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("App.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("App.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Entities.Publication", b =>
                {
                    b.HasOne("App.Entities.Publications.AgeLimit", "AgeLimit")
                        .WithMany()
                        .HasForeignKey("AgeLimitId");

                    b.HasOne("App.Entities.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Entities.Publications.CoverArt", "CoverArt")
                        .WithMany()
                        .HasForeignKey("CoverArtId");

                    b.HasOne("App.Entities.Product", null)
                        .WithOne()
                        .HasForeignKey("App.Entities.Publication", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("App.Entities.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Entities.Publications.PublicationType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("AgeLimit");

                    b.Navigation("Author");

                    b.Navigation("CoverArt");

                    b.Navigation("Publisher");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("App.Entities.Basket", b =>
                {
                    b.Navigation("BasketProducts");
                });

            modelBuilder.Entity("App.Entities.Product", b =>
                {
                    b.Navigation("Album");

                    b.Navigation("BasketProducts");
                });

            modelBuilder.Entity("App.Entities.Products.Album", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("App.Entities.Publications.Genre", b =>
                {
                    b.Navigation("Publications");
                });

            modelBuilder.Entity("App.Entities.User", b =>
                {
                    b.Navigation("Basket");
                });

            modelBuilder.Entity("App.Entities.Publication", b =>
                {
                    b.Navigation("GenresPublications");
                });
#pragma warning restore 612, 618
        }
    }
}
