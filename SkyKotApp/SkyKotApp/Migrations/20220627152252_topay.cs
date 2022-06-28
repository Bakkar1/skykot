using Microsoft.EntityFrameworkCore.Migrations;

namespace SkyKotApp.Migrations
{
    public partial class topay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Rooms",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateTable(
                name: "RenterContracts",
                columns: table => new
                {
                    RenterContractId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    IsPayed = table.Column<bool>(type: "bit", nullable: false),
                    AmountToPay = table.Column<float>(type: "real", nullable: false),
                    RenterRoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenterContracts", x => x.RenterContractId);
                    table.ForeignKey(
                        name: "FK_RenterContracts_RenterRooms_RenterRoomId",
                        column: x => x.RenterRoomId,
                        principalTable: "RenterRooms",
                        principalColumn: "RenterRoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RenterContracts_RenterRoomId",
                table: "RenterContracts",
                column: "RenterRoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RenterContracts");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Rooms",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
