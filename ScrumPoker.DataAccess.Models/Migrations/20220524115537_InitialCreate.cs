using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumPoker.DataAccess.Models.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRooms", x => x.Id);
                });

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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameRoomsPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameRooms_Id",
                table: "GameRooms",
                column: "Id");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameRoomsPlayers");

            migrationBuilder.DropTable(
                name: "GameRooms");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
