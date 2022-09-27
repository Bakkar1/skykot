using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyKotApp.Migrations
{
    public partial class IsAllowToUseTheApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAllowToUseTheApp",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAllowToUseTheApp",
                table: "AspNetUsers");
        }
    }
}
