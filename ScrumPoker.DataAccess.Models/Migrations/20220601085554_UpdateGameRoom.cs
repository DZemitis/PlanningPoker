using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumPoker.DataAccess.Models.Migrations
{
    public partial class UpdateGameRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "GameRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasterIDId = table.Column<int>(type: "int", nullable: false),
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
                name: "IX_Votes_RoundDtoRoundId",
                table: "Votes",
                column: "RoundDtoRoundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "GameRooms");
            
            migrationBuilder.DropTable(
                name: "Rounds");
        }
    }
}
