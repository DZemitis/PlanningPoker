using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumPoker.DataAccess.Models.Migrations
{
    public partial class UpdateTrial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentRoundId",
                table: "GameRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MasterId",
                table: "GameRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    GameRoomId = table.Column<int>(type: "int", nullable: false),
                    Vote = table.Column<int>(type: "int", nullable: false),
                    RoundId = table.Column<int>(type: "int", nullable: false),
                    RoundDtoRoundId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Rounds_RoundDtoRoundId",
                        column: x => x.RoundDtoRoundId,
                        principalTable: "Rounds",
                        principalColumn: "RoundId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameRooms_CurrentRoundId",
                table: "GameRooms",
                column: "CurrentRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRooms_MasterId",
                table: "GameRooms",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_RoundDtoRoundId",
                table: "Votes",
                column: "RoundDtoRoundId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRooms_Players_MasterId",
                table: "GameRooms",
                column: "MasterId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRooms_Rounds_CurrentRoundId",
                table: "GameRooms",
                column: "CurrentRoundId",
                principalTable: "Rounds",
                principalColumn: "RoundId",
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

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropIndex(
                name: "IX_GameRooms_CurrentRoundId",
                table: "GameRooms");

            migrationBuilder.DropIndex(
                name: "IX_GameRooms_MasterId",
                table: "GameRooms");

            migrationBuilder.DropColumn(
                name: "CurrentRoundId",
                table: "GameRooms");

            migrationBuilder.DropColumn(
                name: "MasterId",
                table: "GameRooms");
        }
    }
}
