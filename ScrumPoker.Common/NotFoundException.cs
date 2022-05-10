namespace ScrumPoker.Common;

public class NotFoundException : ScrumPokerException
{
  
    public NotFoundException(int statusCode, object? value = null) =>
        (StatusCode, Value) = (statusCode, value);
    
    protected NotFoundException()
    {
        
    }

    protected NotFoundException(string message) : base(message)
    {
        
    }
}