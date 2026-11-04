using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class DbVersion12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleUpgradeRequests_Users_AdminId",
                table: "RoleUpgradeRequests");

            migrationBuilder.DropIndex(
                name: "IX_RoleUpgradeRequests_AdminId",
                table: "RoleUpgradeRequests");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "RoleUpgradeRequests");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Teams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Games");

            migrationBuilder.AddColumn<Guid>(
                name: "ClubId",
                table: "Teams",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "RoleUpgradeRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RoleUpgradeRequests_AdminId",
                table: "RoleUpgradeRequests",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUpgradeRequests_Users_AdminId",
                table: "RoleUpgradeRequests",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
