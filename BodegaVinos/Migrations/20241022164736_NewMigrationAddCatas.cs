using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BodegaVinos.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationAddCatas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatasIdTesting",
                table: "Wines",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Catas",
                columns: table => new
                {
                    IdTesting = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    InvitedPeople = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catas", x => x.IdTesting);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wines_CatasIdTesting",
                table: "Wines",
                column: "CatasIdTesting");

            migrationBuilder.AddForeignKey(
                name: "FK_Wines_Catas_CatasIdTesting",
                table: "Wines",
                column: "CatasIdTesting",
                principalTable: "Catas",
                principalColumn: "IdTesting");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wines_Catas_CatasIdTesting",
                table: "Wines");

            migrationBuilder.DropTable(
                name: "Catas");

            migrationBuilder.DropIndex(
                name: "IX_Wines_CatasIdTesting",
                table: "Wines");

            migrationBuilder.DropColumn(
                name: "CatasIdTesting",
                table: "Wines");
        }
    }
}
