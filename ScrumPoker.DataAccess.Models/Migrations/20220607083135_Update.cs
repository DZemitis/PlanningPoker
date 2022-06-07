using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumPoker.DataAccess.Models.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Rounds_RoundDtoRoundId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_RoundDtoRoundId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Players_Id_Email",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_GameRooms_CurrentRoundId",
                table: "GameRooms");

            migrationBuilder.DropColumn(
                name: "GameRoomId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "RoundDtoRoundId",
                table: "Votes");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PlayerId",
                table: "Votes",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PlayerId_RoundId",
                table: "Votes",
                columns: new[] { "PlayerId", "RoundId" });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_RoundId",
                table: "Votes",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_RoundId_GameRoomId",
                table: "Rounds",
                columns: new[] { "RoundId", "GameRoomId" });

            migrationBuilder.CreateIndex(
                name: "IX_Players_Id",
                table: "Players",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GameRooms_CurrentRoundId",
                table: "GameRooms",
                column: "CurrentRoundId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Players_PlayerId",
                table: "Votes",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Rounds_RoundId",
                table: "Votes",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "RoundId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Players_PlayerId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Rounds_RoundId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_PlayerId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_PlayerId_RoundId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_RoundId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Rounds_RoundId_GameRoomId",
                table: "Rounds");

            migrationBuilder.DropIndex(
                name: "IX_Players_Id",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_GameRooms_CurrentRoundId",
                table: "GameRooms");

            migrationBuilder.AddColumn<int>(
                name: "GameRoomId",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoundDtoRoundId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Players",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_RoundDtoRoundId",
                table: "Votes",
                column: "RoundDtoRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Id_Email",
                table: "Players",
                columns: new[] { "Id", "Email" });

            migrationBuilder.CreateIndex(
                name: "IX_GameRooms_CurrentRoundId",
                table: "GameRooms",
                column: "CurrentRoundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Rounds_RoundDtoRoundId",
                table: "Votes",
                column: "RoundDtoRoundId",
                principalTable: "Rounds",
                principalColumn: "RoundId");
        }
    }
}
