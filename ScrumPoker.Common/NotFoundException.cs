namespace ScrumPoker.Common;

public class NotFoundException : ScrumPokerException
{
    public NotFoundException(int statusCode, string message) =>
        (StatusCode, Message) = (statusCode, message);
    
    protected NotFoundException()
    {
        
    }

    protected NotFoundException(string message) : base(message)
    {
        
    }
}