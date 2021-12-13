using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class DeleteTableBasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Baskets_BasketId",
                table: "BasketProducts");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.RenameColumn(
                name: "BasketId",
                table: "BasketProducts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProducts_BasketId_ProductId",
                table: "BasketProducts",
                newName: "IX_BasketProducts_UserId_ProductId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d55c1c3f-d22a-490e-b633-96a7cdae1d24");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "75917b8a-f2b6-465a-a767-aff1aaf9a0c1");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_AspNetUsers_UserId",
                table: "BasketProducts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_AspNetUsers_UserId",
                table: "BasketProducts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BasketProducts",
                newName: "BasketId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProducts_UserId_ProductId",
                table: "BasketProducts",
                newName: "IX_BasketProducts_BasketId_ProductId");

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Baskets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "12553e04-861c-4747-92eb-2396f561237c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "14ef6fcb-abfe-4c1d-aafe-fba6a3ba9f8a");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Baskets_BasketId",
                table: "BasketProducts",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
