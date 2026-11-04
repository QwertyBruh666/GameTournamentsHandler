using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStatsFromTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_Team1Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_Team2Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Losses",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "MatchesPlayed",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Wins",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "Team2Score",
                table: "Matches",
                newName: "Participant2Score");

            migrationBuilder.RenameColumn(
                name: "Team2Id",
                table: "Matches",
                newName: "Participant2Id");

            migrationBuilder.RenameColumn(
                name: "Team1Score",
                table: "Matches",
                newName: "Participant1Score");

            migrationBuilder.RenameColumn(
                name: "Team1Id",
                table: "Matches",
                newName: "Participant1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Team2Id",
                table: "Matches",
                newName: "IX_Matches_Participant2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Team1Id",
                table: "Matches",
                newName: "IX_Matches_Participant1Id");

            migrationBuilder.AddColumn<Guid>(
                name: "NextMatchId",
                table: "Matches",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_NextMatchId",
                table: "Matches",
                column: "NextMatchId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Matches_NextMatchId",
                table: "Matches",
                column: "NextMatchId",
                principalTable: "Matches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TournamentMembers_Participant1Id",
                table: "Matches",
                column: "Participant1Id",
                principalTable: "TournamentMembers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TournamentMembers_Participant2Id",
                table: "Matches",
                column: "Participant2Id",
                principalTable: "TournamentMembers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Matches_NextMatchId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TournamentMembers_Participant1Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TournamentMembers_Participant2Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_NextMatchId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "NextMatchId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "Participant2Score",
                table: "Matches",
                newName: "Team2Score");

            migrationBuilder.RenameColumn(
                name: "Participant2Id",
                table: "Matches",
                newName: "Team2Id");

            migrationBuilder.RenameColumn(
                name: "Participant1Score",
                table: "Matches",
                newName: "Team1Score");

            migrationBuilder.RenameColumn(
                name: "Participant1Id",
                table: "Matches",
                newName: "Team1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Participant2Id",
                table: "Matches",
                newName: "IX_Matches_Team2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Participant1Id",
                table: "Matches",
                newName: "IX_Matches_Team1Id");

            migrationBuilder.AddColumn<int>(
                name: "Losses",
                table: "Teams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MatchesPlayed",
                table: "Teams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wins",
                table: "Teams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_Team1Id",
                table: "Matches",
                column: "Team1Id",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_Team2Id",
                table: "Matches",
                column: "Team2Id",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
