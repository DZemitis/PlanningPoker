namespace ScrumPoker.DataAcces.Models.Models;

public class GameRoomDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<PlayerDto> Players { get; set; }
}