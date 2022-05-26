namespace ScrumPoker.DataAccess.Models.Models;

public class GameRoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<GameRoomPlayer> GameRoomPlayers { get; set; } = null!;
    public PlayerDto MasterID { get; set; } = null!;
    public string Story { get; set; } = null!;
    public RoundDto RoundDto { get; set; } = null!;
    public int CurrentRoundId { get; set; }
}