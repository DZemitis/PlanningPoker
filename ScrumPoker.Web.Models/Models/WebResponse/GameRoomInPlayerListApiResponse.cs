using System.Text.Json.Serialization;

namespace ScrumPoker.Web.Models.Models.WebResponse;

public class GameRoomInPlayerListApiResponse
{
    [JsonIgnore] public int Id { get; set; }

    public string? Name { get; set; }
}