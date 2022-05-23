using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class AddLastViewDateToView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastTime",
                table: "Views",
                newName: "LastViewDate");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastViewCountChangeDate",
                table: "Views",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastViewCountChangeDate",
                table: "Views");

            migrationBuilder.RenameColumn(
                name: "LastViewDate",
                table: "Views",
                newName: "LastTime");
        }
    }
}
