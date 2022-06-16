using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumPoker.DataAccess.Models.Migrations
{
    public partial class UpdateOnVoteEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vote",
                table: "Votes",
                newName: "VoteResult");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VoteResult",
                table: "Votes",
                newName: "Vote");
        }
    }
}
