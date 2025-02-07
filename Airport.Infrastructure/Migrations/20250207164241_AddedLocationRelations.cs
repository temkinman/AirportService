using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airport.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedLocationRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Airports_LocationId",
                table: "Airports");

            migrationBuilder.CreateIndex(
                name: "IX_Airports_LocationId",
                table: "Airports",
                column: "LocationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Airports_LocationId",
                table: "Airports");

            migrationBuilder.CreateIndex(
                name: "IX_Airports_LocationId",
                table: "Airports",
                column: "LocationId");
        }
    }
}
