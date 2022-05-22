using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class CreateTableTagGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TagGroupId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TagGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorHex = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagGroupId",
                table: "Tags",
                column: "TagGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TagGroup_ColorHex",
                table: "TagGroup",
                column: "ColorHex",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagGroup_Name",
                table: "TagGroup",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_TagGroup_TagGroupId",
                table: "Tags",
                column: "TagGroupId",
                principalTable: "TagGroup",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_TagGroup_TagGroupId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "TagGroup");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Name",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TagGroupId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagGroupId",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
