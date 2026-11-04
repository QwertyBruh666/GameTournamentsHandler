using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class NullWinner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Teams_WinnerId",
                table: "Tournaments");

            migrationBuilder.AlterColumn<Guid>(
                name: "WinnerId",
                table: "Tournaments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Teams_WinnerId",
                table: "Tournaments",
                column: "WinnerId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Teams_WinnerId",
                table: "Tournaments");

            migrationBuilder.AlterColumn<Guid>(
                name: "WinnerId",
                table: "Tournaments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Teams_WinnerId",
                table: "Tournaments",
                column: "WinnerId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
