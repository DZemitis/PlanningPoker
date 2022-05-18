namespace ScrumPoker.DataAccess.Models.Models;

public class PlayerDtoResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<GameRoomDtoResponse>? GameRooms { get; set; }
}