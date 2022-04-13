using System.Text.Json.Serialization;

namespace ScrumPoker.Core.Models;

public class Player
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Vote { get; set; }
    public bool HasVoted { get; set; }
    // public string Opinion { get; set; }
}