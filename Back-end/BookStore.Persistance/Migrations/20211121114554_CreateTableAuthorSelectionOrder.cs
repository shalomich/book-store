using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class CreateTableAuthorSelectionOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorSelectionOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorSelectionOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorSelectionOrder_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d6be3fa9-868a-470c-9c54-869414fcc3e5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e27b0e9e-e10b-41ba-a043-ed27ba142b31");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorSelectionOrder_AuthorId",
                table: "AuthorSelectionOrder",
                column: "AuthorId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorSelectionOrder");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "61053d86-5260-4eb1-be8d-b4bf028e1a64");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9c1023ac-3e9a-4d59-8b83-4fabad1ee14d");
        }
    }
}
