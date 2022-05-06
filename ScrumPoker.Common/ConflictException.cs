namespace ScrumPoker.Common;

public class ConflictException : ScrumPokerException
{
    protected ConflictException()
    {
        
    }

    protected ConflictException(string message) : base(message)
    {
        
    }
}