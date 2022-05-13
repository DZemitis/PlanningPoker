using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumPoker.Data.Migrations
{
    public partial class AddLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameRoomDtoPlayerDto",
                columns: table => new
                {
                    GameRoomsId = table.Column<int>(type: "int", nullable: false),
                    PlayersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRoomDtoPlayerDto", x => new { x.GameRoomsId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_GameRoomDtoPlayerDto_GameRooms_GameRoomsId",
                        column: x => x.GameRoomsId,
                        principalTable: "GameRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameRoomDtoPlayerDto_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameRoomDtoPlayerDto_PlayersId",
                table: "GameRoomDtoPlayerDto",
                column: "PlayersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameRoomDtoPlayerDto");
        }
    }
}
