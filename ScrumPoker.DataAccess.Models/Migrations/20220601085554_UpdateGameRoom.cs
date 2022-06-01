

#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using ScrumPoker.DataAccess.Models.Models;

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
            
            migrationBuilder.AddColumn<int>("MasterId", "GameRooms", type:"int", nullable: false);
            migrationBuilder.AddColumn<int>("RoundDtoRoundId", "GameRooms", type: "int", nullable: false);
            migrationBuilder.AddColumn<int>("CurrentRoundId", "GameRooms", type: "int", nullable: false);
            
            migrationBuilder.AddForeignKey("FK_GameRooms_Players_MasterId", 
                "GameRooms", 
                "MasterId",
                "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            
            migrationBuilder.AddForeignKey("IX_GameRooms_RoundDtoRoundId", 
                "GameRooms", 
                "RoundDtoRoundId",
                "Rounds",
                principalColumn:"RoundId",
                onDelete: ReferentialAction.Cascade);
            
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
                name: "IX_GameRooms_MasterId",
                table: "GameRooms",
                column: "MasterId");

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
                name: "Rounds");
        }
    }
}
