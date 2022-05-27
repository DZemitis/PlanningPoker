using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumPoker.DataAccess.Models.Migrations
{
    public partial class UpdateGameRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    RoundId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoundState = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.RoundId);
                });

            migrationBuilder.CreateTable(
                name: "VoteReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoteRegistrationId = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteReviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasterIDId = table.Column<int>(type: "int", nullable: false),
                    Story = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoundDtoRoundId = table.Column<int>(type: "int", nullable: false),
                    CurrentRoundId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRooms_Players_MasterIDId",
                        column: x => x.MasterIDId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameRooms_Rounds_RoundDtoRoundId",
                        column: x => x.RoundDtoRoundId,
                        principalTable: "Rounds",
                        principalColumn: "RoundId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "GameRoomsPlayers",
                columns: table => new
                {
                    GameRoomId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRoomsPlayers", x => new { x.GameRoomId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_GameRoomsPlayers_GameRooms_GameRoomId",
                        column: x => x.GameRoomId,
                        principalTable: "GameRooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameRoomsPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameRooms_Id",
                table: "GameRooms",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GameRooms_MasterIDId",
                table: "GameRooms",
                column: "MasterIDId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRooms_RoundDtoRoundId",
                table: "GameRooms",
                column: "RoundDtoRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRoomsPlayers_GameRoomId_PlayerId",
                table: "GameRoomsPlayers",
                columns: new[] { "GameRoomId", "PlayerId" });

            migrationBuilder.CreateIndex(
                name: "IX_GameRoomsPlayers_PlayerId",
                table: "GameRoomsPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Id_Email",
                table: "Players",
                columns: new[] { "Id", "Email" });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_RoundDtoRoundId",
                table: "Votes",
                column: "RoundDtoRoundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameRoomsPlayers");

            migrationBuilder.DropTable(
                name: "VoteReviews");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "GameRooms");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Rounds");
        }
    }
}
