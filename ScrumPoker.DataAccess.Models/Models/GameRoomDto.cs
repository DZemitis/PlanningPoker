namespace ScrumPoker.DataAccess.Models.Models;

public class GameRoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<GameRoomPlayer> GameRoomPlayers { get; set; } = null!;
    public int? MasterId { get; set; }
    public PlayerDto? Master { get; set; }
    public int? CurrentRoundId { set; get; }
    public RoundDto? CurrentRound { get; set; }
    public List<RoundDto> Rounds { get; set; } = null!;
}