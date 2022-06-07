using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumPoker.DataAccess.Models.Migrations
{
    public partial class Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PLayersVoteId",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PlayerId",
                table: "Votes",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PLayersVoteId",
                table: "Players",
                column: "PLayersVoteId");

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
                name: "FK_Players_Votes_PLayersVoteId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Votes_PlayerId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Players_PLayersVoteId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PLayersVoteId",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PlayerId",
                table: "Votes",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PlayerId_RoundId",
                table: "Votes",
                columns: new[] { "PlayerId", "RoundId" });
        }
    }
}
