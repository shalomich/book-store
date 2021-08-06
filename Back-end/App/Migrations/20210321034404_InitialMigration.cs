using System;
using Microsoft.EntityFrameworkCore.Migrations;
using App;

namespace Storage.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "entities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TitleImageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeathDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_authors_entities_Id",
                        column: x => x.Id,
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Encoding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_images_entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publishers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_publishers_entities_Id",
                        column: x => x.Id,
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "publications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Genres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageQuantity = table.Column<int>(type: "int", nullable: true),
                    CoverArt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeLimit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicationFormat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_publications_authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_publications_entities_Id",
                        column: x => x.Id,
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_publications_publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_images_EntityId",
                table: "images",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_publications_AuthorId",
                table: "publications",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_publications_ISBN",
                table: "publications",
                column: "ISBN",
                unique: true,
                filter: "[ISBN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_publications_PublisherId",
                table: "publications",
                column: "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "publications");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropTable(
                name: "publishers");

            migrationBuilder.DropTable(
                name: "entities");
        }
    }
}
