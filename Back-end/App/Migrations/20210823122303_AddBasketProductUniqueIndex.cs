using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class AddBasketProductUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BasketProducts_BasketId",
                table: "BasketProducts");

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

            migrationBuilder.CreateIndex(
                name: "IX_BasketProducts_BasketId_ProductId",
                table: "BasketProducts",
                columns: new[] { "BasketId", "ProductId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BasketProducts_BasketId_ProductId",
                table: "BasketProducts");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7d5002cc-1611-4010-8102-f83cd6efeba8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "78216bae-c0f2-460f-84fe-60e6d12834fd");

            migrationBuilder.CreateIndex(
                name: "IX_BasketProducts_BasketId",
                table: "BasketProducts",
                column: "BasketId");
        }
    }
}
