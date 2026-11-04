using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class FixedNextMatchConstr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_NextMatchId",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_NextMatchId",
                table: "Matches",
                column: "NextMatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_NextMatchId",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_NextMatchId",
                table: "Matches",
                column: "NextMatchId",
                unique: true);
        }
    }
}
