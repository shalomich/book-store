using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeathDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleImageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleImageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "publications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    TitleImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                        name: "FK_publications_publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endoding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    PublicationId = table.Column<int>(type: "int", nullable: true),
                    PublisherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_authors_Name_Surname_BirthDate",
                table: "authors",
                columns: new[] { "Name", "Surname", "BirthDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_AuthorId",
                table: "Image",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_PublicationId",
                table: "Image",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_PublisherId",
                table: "Image",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_publications_AuthorId",
                table: "publications",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_publications_ISBN",
                table: "publications",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_publications_Name_ReleaseYear_AuthorId_PublisherId",
                table: "publications",
                columns: new[] { "Name", "ReleaseYear", "AuthorId", "PublisherId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_publications_PublisherId",
                table: "publications",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_publishers_Name",
                table: "publishers",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "publications");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropTable(
                name: "publishers");
        }
    }
}
