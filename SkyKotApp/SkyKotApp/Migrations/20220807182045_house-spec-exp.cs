using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyKotApp.Migrations
{
    public partial class housespecexp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HouseExpenses",
                columns: table => new
                {
                    HouseExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    ExpenceId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseExpenses", x => x.HouseExpenseId);
                    table.ForeignKey(
                        name: "FK_HouseExpenses_Expences_ExpenceId",
                        column: x => x.ExpenceId,
                        principalTable: "Expences",
                        principalColumn: "ExpenceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseExpenses_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "HouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseSpecifications",
                columns: table => new
                {
                    HouseSpecificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    SpecificationId = table.Column<int>(type: "int", nullable: false),
                    IsAvailAble = table.Column<bool>(type: "bit", nullable: false),
                    WhereAvailAble = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseSpecifications", x => x.HouseSpecificationId);
                    table.ForeignKey(
                        name: "FK_HouseSpecifications_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "HouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseSpecifications_Specifications_SpecificationId",
                        column: x => x.SpecificationId,
                        principalTable: "Specifications",
                        principalColumn: "SpecificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseExpenses_ExpenceId",
                table: "HouseExpenses",
                column: "ExpenceId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseExpenses_HouseId",
                table: "HouseExpenses",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseSpecifications_HouseId",
                table: "HouseSpecifications",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseSpecifications_SpecificationId",
                table: "HouseSpecifications",
                column: "SpecificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HouseExpenses");

            migrationBuilder.DropTable(
                name: "HouseSpecifications");
        }
    }
}
