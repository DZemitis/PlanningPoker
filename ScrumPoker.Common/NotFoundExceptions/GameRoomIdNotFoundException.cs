namespace ScrumPoker.Common.NotFoundExceptions;

public class GameRoomIdNotFoundException : NotFoundException
{
    public GameRoomIdNotFoundException() : base("Game room ID not found")
    {
        
    }
}