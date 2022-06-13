using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumPoker.DataAccess.Models.Migrations
{
    public partial class MigrationsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_Id_Email",
                table: "Players");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "PLayersVoteId",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentRoundId",
                table: "GameRooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MasterId",
                table: "GameRooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    RoundId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameRoomId = table.Column<int>(type: "int", nullable: false),
                    RoundState = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.RoundId);
                    table.ForeignKey(
                        name: "FK_Rounds_GameRooms_GameRoomId",
                        column: x => x.GameRoomId,
                        principalTable: "GameRooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Vote = table.Column<int>(type: "int", nullable: false),
                    RoundId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "RoundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_Id",
                table: "Players",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PLayersVoteId",
                table: "Players",
                column: "PLayersVoteId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRooms_CurrentRoundId",
                table: "GameRooms",
                column: "CurrentRoundId",
                unique: true,
                filter: "[CurrentRoundId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GameRooms_MasterId",
                table: "GameRooms",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_GameRoomId",
                table: "Rounds",
                column: "GameRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_RoundId_GameRoomId",
                table: "Rounds",
                columns: new[] { "RoundId", "GameRoomId" });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PlayerId",
                table: "Votes",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_RoundId",
                table: "Votes",
                column: "RoundId");

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
                name: "FK_Players_Votes_PLayersVoteId",
                table: "Players",
                column: "PLayersVoteId",
                principalTable: "Votes",
                principalColumn: "Id");
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
                name: "FK_Players_Votes_PLayersVoteId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropIndex(
                name: "IX_Players_Id",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_PLayersVoteId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_GameRooms_CurrentRoundId",
                table: "GameRooms");

            migrationBuilder.DropIndex(
                name: "IX_GameRooms_MasterId",
                table: "GameRooms");

            migrationBuilder.DropColumn(
                name: "PLayersVoteId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CurrentRoundId",
                table: "GameRooms");

            migrationBuilder.DropColumn(
                name: "MasterId",
                table: "GameRooms");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Players",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Id_Email",
                table: "Players",
                columns: new[] { "Id", "Email" });
        }
    }
}
