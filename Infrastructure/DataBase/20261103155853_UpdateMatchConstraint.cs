using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.DataBase
{
    /// <inheritdoc />
    public partial class UpdateMatchConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_Participant1Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Participant2Id",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Participant1Id",
                table: "Matches",
                column: "Participant1Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Participant2Id",
                table: "Matches",
                column: "Participant2Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_Participant1Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Participant2Id",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Participant1Id",
                table: "Matches",
                column: "Participant1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Participant2Id",
                table: "Matches",
                column: "Participant2Id");
        }
    }
}
