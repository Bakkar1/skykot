using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyKotApp.Migrations
{
    public partial class testStop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStoped",
                table: "RenterRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StopDate",
                table: "RenterRooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStoped",
                table: "RenterRooms");

            migrationBuilder.DropColumn(
                name: "StopDate",
                table: "RenterRooms");
        }
    }
}
