using Microsoft.EntityFrameworkCore.Migrations;

namespace CalculationTools.Data.Migrations
{
    public partial class AddedCharacterServerAndWorldTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterName = table.Column<string>(nullable: true),
                    CharacterOwnerId = table.Column<int>(nullable: false),
                    CharacterOwnerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServerName = table.Column<string>(nullable: true),
                    ServerCountryCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Worlds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorldCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Full = table.Column<bool>(nullable: false),
                    AllowLogin = table.Column<bool>(nullable: false),
                    Maintenance = table.Column<bool>(nullable: false),
                    Recommended = table.Column<int>(nullable: false),
                    KeyRequired = table.Column<bool>(nullable: false),
                    ServerId = table.Column<int>(nullable: true),
                    CharacterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worlds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Worlds_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Worlds_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 1, "en", "International" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 17, "ro", "Romania" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 16, "sk", "Slovakia" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 15, "no", "Norway" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 14, "pt", "Portugal" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 13, "cz", "Czech Republic" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 12, "tr", "Turkey" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 11, "gr", "Greece" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 18, "hu", "Hungary" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 10, "it", "Italy" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 8, "us", "United States" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 7, "ru", "Russia" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 6, "fr", "France" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 5, "pl", "Poland" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 4, "br", "Brazil" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 3, "de", "Germany" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 2, "nl", "Netherlands" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 9, "es", "Spain" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerCountryCode", "ServerName" },
                values: new object[] { 19, "beta", "Beta Server" });

            migrationBuilder.CreateIndex(
                name: "IX_Worlds_CharacterId",
                table: "Worlds",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Worlds_ServerId",
                table: "Worlds",
                column: "ServerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Worlds");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Servers");
        }
    }
}
