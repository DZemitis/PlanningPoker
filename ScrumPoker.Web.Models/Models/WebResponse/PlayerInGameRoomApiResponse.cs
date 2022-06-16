using System.Text.Json.Serialization;

namespace ScrumPoker.Web.Models.Models.WebResponse;

public class PlayerInGameRoomApiResponse
{
    [JsonIgnore] public int Id { get; set; }

    public string? Name { get; set; }
    public string? Email { get; set; }
}