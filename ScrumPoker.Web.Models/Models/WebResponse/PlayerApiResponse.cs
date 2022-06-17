namespace ScrumPoker.Web.Models.Models.WebResponse;

public class PlayerApiResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public List<GameRoomInPlayerListApiResponse>? GameRooms { get; set; }
}