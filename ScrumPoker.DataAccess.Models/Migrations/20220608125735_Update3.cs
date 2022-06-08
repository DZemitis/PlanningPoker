using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumPoker.DataAccess.Models.Migrations
{
    public partial class Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GameRooms_CurrentRoundId",
                table: "GameRooms");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentRoundId",
                table: "GameRooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_GameRooms_CurrentRoundId",
                table: "GameRooms",
                column: "CurrentRoundId",
                unique: true,
                filter: "[CurrentRoundId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GameRooms_CurrentRoundId",
                table: "GameRooms");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentRoundId",
                table: "GameRooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameRooms_CurrentRoundId",
                table: "GameRooms",
                column: "CurrentRoundId",
                unique: true);
        }
    }
}
