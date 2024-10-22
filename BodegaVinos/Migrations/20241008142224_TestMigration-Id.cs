using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BodegaVinos.Migrations
{
    /// <inheritdoc />
    public partial class TestMigrationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Wines",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Wines",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Wines",
                keyColumn: "Id",
                keyValue: -1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Wines",
                columns: new[] { "Id", "CreatedAt", "Name", "Region", "Stock", "Variety", "Year" },
                values: new object[,]
                {
                    { -3, new DateTime(2024, 10, 8, 14, 13, 57, 472, DateTimeKind.Utc).AddTicks(188), "El Enemigo Bonarda", "San Juan", 0, "Bonarda", 2010 },
                    { -2, new DateTime(2024, 10, 8, 14, 13, 57, 472, DateTimeKind.Utc).AddTicks(183), "Catena Zapata Cabernet Sauvignon", "Mendoza", 5, "Cabernet Sauvignon", 2000 },
                    { -1, new DateTime(2024, 10, 8, 14, 13, 57, 472, DateTimeKind.Utc).AddTicks(174), "Luigi Bosca Malbec", "Mendoza", 25, "Malbec", 2021 }
                });
        }
    }
}
