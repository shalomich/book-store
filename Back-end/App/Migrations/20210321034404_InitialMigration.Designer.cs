﻿// <auto-generated />
using System;
using App;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210321034404_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Storage.Models.Entity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TitleImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("entities");
                });

            modelBuilder.Entity("Storage.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Encoding")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntityId")
                        .HasColumnType("int");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.ToTable("images");
                });

            modelBuilder.Entity("Storage.Models.Author", b =>
                {
                    b.HasBaseType("Storage.Models.Entity");

                    b.Property<string>("Biography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeathDate")
                        .HasColumnType("datetime2");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("authors");
                });

            modelBuilder.Entity("Storage.Models.Publication", b =>
                {
                    b.HasBaseType("Storage.Models.Entity");

                    b.Property<DateTime>("AddingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AgeLimit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<string>("CoverArt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
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

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ISBN")
                        .IsUnique()
                        .HasFilter("[ISBN] IS NOT NULL");

                    b.HasIndex("PublisherId");

                    b.HasIndex("Name", "ReleaseYear", "AuthorId", "PublisherId")
                        .IsUnique();

                    b.ToTable("publications");
                });

            modelBuilder.Entity("Storage.Models.Publisher", b =>
                {
                    b.HasBaseType("Storage.Models.Entity");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("publishers");
                });

            modelBuilder.Entity("Storage.Models.Image", b =>
                {
                    b.HasOne("Storage.Models.Entity", "Entity")
                        .WithMany("Images")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("Storage.Models.Author", b =>
                {
                    b.HasOne("Storage.Models.Entity", null)
                        .WithOne()
                        .HasForeignKey("Storage.Models.Author", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Storage.Models.Publication", b =>
                {
                    b.HasOne("Storage.Models.Author", "Author")
                        .WithMany("Publications")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Storage.Models.Entity", null)
                        .WithOne()
                        .HasForeignKey("Storage.Models.Publication", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Storage.Models.Publisher", "Publisher")
                        .WithMany("Publications")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("Storage.Models.Publisher", b =>
                {
                    b.HasOne("Storage.Models.Entity", null)
                        .WithOne()
                        .HasForeignKey("Storage.Models.Publisher", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Storage.Models.Entity", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Storage.Models.Author", b =>
                {
                    b.Navigation("Publications");
                });

            modelBuilder.Entity("Storage.Models.Publisher", b =>
                {
                    b.Navigation("Publications");
                });
#pragma warning restore 612, 618
        }
    }
}