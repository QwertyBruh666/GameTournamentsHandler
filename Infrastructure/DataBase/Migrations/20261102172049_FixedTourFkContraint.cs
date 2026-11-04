using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class FixedTourFkContraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tournaments_OrganizerId",
                table: "Tournaments");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_OrganizerId",
                table: "Tournaments",
                column: "OrganizerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tournaments_OrganizerId",
                table: "Tournaments");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_OrganizerId",
                table: "Tournaments",
                column: "OrganizerId",
                unique: true);
        }
    }
}
