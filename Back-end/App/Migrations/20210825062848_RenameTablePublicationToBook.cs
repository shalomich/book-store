using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class RenameTablePublicationToBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenrePublication");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "PublicationTypes");

            migrationBuilder.CreateTable(
                name: "BookTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    OriginalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeLimitId = table.Column<int>(type: "int", nullable: true),
                    CoverArtId = table.Column<int>(type: "int", nullable: true),
                    BookFormat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageQuantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_AgeLimits_AgeLimitId",
                        column: x => x.AgeLimitId,
                        principalTable: "AgeLimits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_BookTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "BookTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_CoverArts_CoverArtId",
                        column: x => x.CoverArtId,
                        principalTable: "CoverArts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Products_Id",
                        column: x => x.Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenreBook_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreBook_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "23578efd-5f14-4054-8e9d-fbd5e1921c2c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e0558110-6e10-4132-9d64-6e370c3a839e");

            migrationBuilder.InsertData(
                table: "BookTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Художественная литература" },
                    { 2, "Манга" },
                    { 3, "Ранобэ" },
                    { 4, "Графический роман" },
                    { 5, "Артбук" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AgeLimitId",
                table: "Books",
                column: "AgeLimitId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CoverArtId",
                table: "Books",
                column: "CoverArtId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Isbn",
                table: "Books",
                column: "Isbn",
                unique: true,
                filter: "[Isbn] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_TypeId",
                table: "Books",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTypes_Name",
                table: "BookTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GenreBook_BookId",
                table: "GenreBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreBook_GenreId",
                table: "GenreBook",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreBook");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "BookTypes");

            migrationBuilder.CreateTable(
                name: "PublicationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AgeLimitId = table.Column<int>(type: "int", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    CoverArtId = table.Column<int>(type: "int", nullable: true),
                    Isbn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OriginalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageQuantity = table.Column<int>(type: "int", nullable: true),
                    PublicationFormat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publications_AgeLimits_AgeLimitId",
                        column: x => x.AgeLimitId,
                        principalTable: "AgeLimits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publications_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Publications_CoverArts_CoverArtId",
                        column: x => x.CoverArtId,
                        principalTable: "CoverArts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publications_Products_Id",
                        column: x => x.Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publications_PublicationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PublicationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publications_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenrePublication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    PublicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenrePublication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenrePublication_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenrePublication_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "70e7f8d5-bd14-443b-8f21-13c83adc1815");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a59b9e24-0ce5-4e75-a871-26665dbed366");

            migrationBuilder.InsertData(
                table: "PublicationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Книга" },
                    { 2, "Манга" },
                    { 3, "Ранобэ" },
                    { 4, "Графический роман" },
                    { 5, "Артбук" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenrePublication_GenreId",
                table: "GenrePublication",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GenrePublication_PublicationId",
                table: "GenrePublication",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_AgeLimitId",
                table: "Publications",
                column: "AgeLimitId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_AuthorId",
                table: "Publications",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_CoverArtId",
                table: "Publications",
                column: "CoverArtId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_Isbn",
                table: "Publications",
                column: "Isbn",
                unique: true,
                filter: "[Isbn] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_PublisherId",
                table: "Publications",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_TypeId",
                table: "Publications",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationTypes_Name",
                table: "PublicationTypes",
                column: "Name",
                unique: true);
        }
    }
}
