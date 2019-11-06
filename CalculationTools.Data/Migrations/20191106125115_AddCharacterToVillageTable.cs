using Microsoft.EntityFrameworkCore.Migrations;

namespace CalculationTools.Data.Migrations
{
    public partial class AddCharacterToVillageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Villages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Villages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Villages_CharacterId",
                table: "Villages",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Villages_Characters_CharacterId",
                table: "Villages",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Villages_Characters_CharacterId",
                table: "Villages");

            migrationBuilder.DropIndex(
                name: "IX_Villages_CharacterId",
                table: "Villages");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Villages");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Villages");
        }
    }
}
