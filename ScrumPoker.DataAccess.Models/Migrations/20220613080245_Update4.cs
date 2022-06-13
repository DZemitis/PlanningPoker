using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumPoker.DataAccess.Models.Migrations
{
    public partial class Update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRooms_Players_MasterId",
                table: "GameRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRooms_Rounds_CurrentRoundId",
                table: "GameRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_GameRooms_GameRoomId",
                table: "Rounds");

            migrationBuilder.AlterColumn<int>(
                name: "MasterId",
                table: "GameRooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRooms_Players_MasterId",
                table: "GameRooms",
                column: "MasterId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRooms_Rounds_CurrentRoundId",
                table: "GameRooms",
                column: "CurrentRoundId",
                principalTable: "Rounds",
                principalColumn: "RoundId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_GameRooms_GameRoomId",
                table: "Rounds",
                column: "GameRoomId",
                principalTable: "GameRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRooms_Players_MasterId",
                table: "GameRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRooms_Rounds_CurrentRoundId",
                table: "GameRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_GameRooms_GameRoomId",
                table: "Rounds");

            migrationBuilder.AlterColumn<int>(
                name: "MasterId",
                table: "GameRooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRooms_Players_MasterId",
                table: "GameRooms",
                column: "MasterId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRooms_Rounds_CurrentRoundId",
                table: "GameRooms",
                column: "CurrentRoundId",
                principalTable: "Rounds",
                principalColumn: "RoundId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_GameRooms_GameRoomId",
                table: "Rounds",
                column: "GameRoomId",
                principalTable: "GameRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
