using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class AddColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_publishers_Name",
                table: "publishers");

            migrationBuilder.DropIndex(
                name: "IX_publications_Name_ReleaseYear_AuthorId_PublisherId",
                table: "publications");

            migrationBuilder.DropIndex(
                name: "IX_authors_Name_Surname_BirthDate",
                table: "authors");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "authors");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "authors");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "publishers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "publications",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "authors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_publishers_Name",
                table: "publishers",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_publications_Name_ReleaseYear_AuthorId_PublisherId",
                table: "publications",
                columns: new[] { "Name", "ReleaseYear", "AuthorId", "PublisherId" },
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_authors_Name",
                table: "authors",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_publishers_Name",
                table: "publishers");

            migrationBuilder.DropIndex(
                name: "IX_publications_Name_ReleaseYear_AuthorId_PublisherId",
                table: "publications");

            migrationBuilder.DropIndex(
                name: "IX_authors_Name",
                table: "authors");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "publishers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "publications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "authors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "authors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_publishers_Name",
                table: "publishers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_publications_Name_ReleaseYear_AuthorId_PublisherId",
                table: "publications",
                columns: new[] { "Name", "ReleaseYear", "AuthorId", "PublisherId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_authors_Name_Surname_BirthDate",
                table: "authors",
                columns: new[] { "Name", "Surname", "BirthDate" },
                unique: true);
        }
    }
}
