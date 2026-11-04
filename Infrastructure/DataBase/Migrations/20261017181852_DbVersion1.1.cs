using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class DbVersion11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TeamMembers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RoleUpgradeRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AdminId = table.Column<Guid>(type: "uuid", nullable: false),
                    NewRole = table.Column<int>(type: "integer", nullable: false),
                    RequesterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUpgradeRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleUpgradeRequests_Users_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUpgradeRequests_Users_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_UserId",
                table: "TeamMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUpgradeRequests_AdminId",
                table: "RoleUpgradeRequests",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUpgradeRequests_RequesterId",
                table: "RoleUpgradeRequests",
                column: "RequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Users_UserId",
                table: "TeamMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Users_UserId",
                table: "TeamMembers");

            migrationBuilder.DropTable(
                name: "RoleUpgradeRequests");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembers_UserId",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TeamMembers");
        }
    }
}
