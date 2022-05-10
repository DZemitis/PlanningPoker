namespace ScrumPoker.Common;

public class ConflictException : ScrumPokerException
{
    public ConflictException(int statusCode, object? value = null) =>
        (StatusCode, Value) = (statusCode, value);
    
    protected ConflictException()
    {
        
    }

    protected ConflictException(string message) : base(message)
    {
        
    }
}