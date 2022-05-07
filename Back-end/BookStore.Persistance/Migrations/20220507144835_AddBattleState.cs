using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class AddBattleState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Battles");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Battles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Battles");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Battles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
