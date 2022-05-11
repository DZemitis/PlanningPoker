namespace ScrumPoker.DataAcces.Models.Models;

public class GameRoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<PlayerDto> Players = new List<PlayerDto>();
}