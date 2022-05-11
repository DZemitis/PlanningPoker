
namespace ScrumPoker.DataAcces.Models.Models;

public class PlayerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<GameRoomDto> GameRooms = new();
}