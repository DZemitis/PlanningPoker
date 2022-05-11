namespace ScrumPoker.Common;

public class ConflictException : ScrumPokerException
{
    public ConflictException(string message) =>
        (Message) = (message);
    
    protected ConflictException()
    {
        
    }
}