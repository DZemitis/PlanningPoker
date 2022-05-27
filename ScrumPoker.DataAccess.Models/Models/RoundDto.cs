using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ScrumPoker.Common.Models;

namespace ScrumPoker.DataAccess.Models.Models;

public class RoundDto
{
    [Key]
    public int RoundId { get; set; }
    public RoundState RoundState { get; set; }
    public string Description { get; set; } = null!;
    public List<VoteRegistrationDto> Votes { get; set; } = null!;
}