namespace ScrumPoker.Common.ConflictExceptions;

public class InvalidRoundStateException : ConflictException
{
    public InvalidRoundStateException(string message)
    {
        Message = message;
    }
}
    
