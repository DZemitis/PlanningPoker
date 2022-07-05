namespace ScrumPoker.Common.ConflictExceptions;

public class InvalidRoundStateException : ConflictException
{
    public InvalidRoundStateException()
    {
    }
    public InvalidRoundStateException(string message) : base(message)
    {
    }
}
    
