using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNavigationProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Clubs_ClubId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "MatchResults");

            migrationBuilder.DropTable(
                name: "TeamStatistics");

            migrationBuilder.DropTable(
                name: "TeamTournament");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_OrganizerId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ClubId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "EndsAt",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "StartsAt",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "ParticipantsCount",
                table: "Tournaments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Tournaments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WinnerId",
                table: "Tournaments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ClubId",
                table: "Teams",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "TeamMembers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "TeamMembers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualEndsAt",
                table: "Matches",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualStartsAt",
                table: "Matches",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Matches",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Round",
                table: "Matches",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduleEndsAt",
                table: "Matches",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduleStartsAt",
                table: "Matches",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Team1Score",
                table: "Matches",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Team2Score",
                table: "Matches",
                type: "double precision",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TournamentMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    TournamentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentMembers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentMembers_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_OrganizerId",
                table: "Tournaments",
                column: "OrganizerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_UserId",
                table: "Tournaments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_WinnerId",
                table: "Tournaments",
                column: "WinnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_UserId1",
                table: "TeamMembers",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentMembers_TeamId",
                table: "TournamentMembers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentMembers_TournamentId",
                table: "TournamentMembers",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Users_UserId1",
                table: "TeamMembers",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Teams_WinnerId",
                table: "Tournaments",
                column: "WinnerId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Users_UserId",
                table: "Tournaments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Users_UserId1",
                table: "TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Teams_WinnerId",
                table: "Tournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Users_UserId",
                table: "Tournaments");

            migrationBuilder.DropTable(
                name: "TournamentMembers");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_OrganizerId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_UserId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_WinnerId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembers_UserId1",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "ParticipantsCount",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Losses",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "MatchesPlayed",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Wins",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "ActualEndsAt",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ActualStartsAt",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Round",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ScheduleEndsAt",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ScheduleStartsAt",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Team1Score",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Team2Score",
                table: "Matches");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClubId",
                table: "Teams",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "TeamMembers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndsAt",
                table: "Matches",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartsAt",
                table: "Matches",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Logo = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    WinnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    Team1Score = table.Column<double>(type: "double precision", nullable: false),
                    Team2Score = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchResults_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchResults_Teams_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Losses = table.Column<int>(type: "integer", nullable: false),
                    MatchesPlayed = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    Wins = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamStatistics_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamTournament",
                columns: table => new
                {
                    TeamsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TournamentsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamTournament", x => new { x.TeamsId, x.TournamentsId });
                    table.ForeignKey(
                        name: "FK_TeamTournament_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamTournament_Tournaments_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_OrganizerId",
                table: "Tournaments",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ClubId",
                table: "Teams",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchResults_MatchId",
                table: "MatchResults",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchResults_WinnerId",
                table: "MatchResults",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStatistics_TeamId",
                table: "TeamStatistics",
                column: "TeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamTournament_TournamentsId",
                table: "TeamTournament",
                column: "TournamentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Clubs_ClubId",
                table: "Teams",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
