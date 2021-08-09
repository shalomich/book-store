using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class AddTableAlbum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_entities_EntityId",
                table: "images");

            migrationBuilder.DropColumn(
                name: "TitleImageName",
                table: "entities");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "images",
                newName: "AlbumId");

            migrationBuilder.RenameIndex(
                name: "IX_images_EntityId",
                table: "images",
                newName: "IX_images_AlbumId");

            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "publications",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "images",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleImageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "335f4312-495c-4d32-a1cf-21789da8328d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "96ebd30e-4b67-4fde-bc2b-b96b9c67cec0");

            migrationBuilder.CreateIndex(
                name: "IX_publications_AlbumId",
                table: "publications",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_images_Name_AlbumId",
                table: "images",
                columns: new[] { "Name", "AlbumId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_images_Album_AlbumId",
                table: "images",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_publications_Album_AlbumId",
                table: "publications",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_Album_AlbumId",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_publications_Album_AlbumId",
                table: "publications");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropIndex(
                name: "IX_publications_AlbumId",
                table: "publications");

            migrationBuilder.DropIndex(
                name: "IX_images_Name_AlbumId",
                table: "images");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "publications");

            migrationBuilder.RenameColumn(
                name: "AlbumId",
                table: "images",
                newName: "EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_images_AlbumId",
                table: "images",
                newName: "IX_images_EntityId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "images",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "TitleImageName",
                table: "entities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "3f9be233-74ac-49b0-b5c3-869ef110cdc4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "61e7fe50-c33f-4202-98b0-f346a2ebf9aa");

            migrationBuilder.AddForeignKey(
                name: "FK_images_entities_EntityId",
                table: "images",
                column: "EntityId",
                principalTable: "entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
