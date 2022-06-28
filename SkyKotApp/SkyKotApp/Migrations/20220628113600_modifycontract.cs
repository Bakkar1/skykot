using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkyKotApp.Migrations
{
    public partial class modifycontract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "RenterContracts");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "RenterContracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "RenterContracts");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "RenterContracts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
