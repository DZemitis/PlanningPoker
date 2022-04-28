namespace ScrumPoker.DataAcces.Models.Models;

public class PlayerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<GameRoomDto> GameRooms = new List<GameRoomDto>();
}