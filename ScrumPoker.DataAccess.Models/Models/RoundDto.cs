using System.ComponentModel.DataAnnotations;
using ScrumPoker.Common.Models;

namespace ScrumPoker.DataAccess.Models.Models;

public class RoundDto
{
    [Key]
    public int RoundId { get; set; }
    public int GameRoomId { get; set; }
    public RoundState RoundState { get; set; }
    public string Description { get; set; } = null!;
    public List<VoteRegistrationDto> Votes { get; set; } = null!;
}