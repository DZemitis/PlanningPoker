namespace ScrumPoker.Common;

public class ConflictException : ScrumPokerException
{
    public ConflictException(int statusCode, string message) =>
        (StatusCode, Message) = (statusCode, message);
    
    protected ConflictException()
    {
        
    }

    protected ConflictException(string message) : base(message)
    {
        
    }
}