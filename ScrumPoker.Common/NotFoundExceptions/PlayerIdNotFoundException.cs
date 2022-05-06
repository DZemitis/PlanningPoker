namespace ScrumPoker.Common.NotFoundExceptions;

public class PlayerIdNotFoundException : NotFoundException
{
    public PlayerIdNotFoundException() : base("Player ID not found")
    {
        
    }
    
    public PlayerIdNotFoundException(string message) : base(message)
    {
    }
}