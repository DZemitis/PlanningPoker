using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumPoker.DataAccess.Models.Migrations
{
    public partial class Update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rounds_GameRoomId",
                table: "Rounds",
                column: "GameRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_GameRooms_GameRoomId",
                table: "Rounds",
                column: "GameRoomId",
                principalTable: "GameRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_GameRooms_GameRoomId",
                table: "Rounds");

            migrationBuilder.DropIndex(
                name: "IX_Rounds_GameRoomId",
                table: "Rounds");
        }
    }
}
