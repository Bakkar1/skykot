using Microsoft.EntityFrameworkCore.Migrations;

namespace SkyKotApp.Migrations
{
    public partial class addToPayToRenterRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountToPay",
                table: "RenterContracts");

            migrationBuilder.AddColumn<float>(
                name: "AmountToPay",
                table: "RenterRooms",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountToPay",
                table: "RenterRooms");

            migrationBuilder.AddColumn<float>(
                name: "AmountToPay",
                table: "RenterContracts",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
