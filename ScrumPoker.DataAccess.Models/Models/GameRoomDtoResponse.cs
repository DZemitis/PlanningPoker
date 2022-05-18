namespace ScrumPoker.DataAccess.Models.Models;

public class GameRoomDtoResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<PlayerDtoResponse>? Players { get; set; }
}