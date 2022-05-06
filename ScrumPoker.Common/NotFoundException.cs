namespace ScrumPoker.Common;

public class NotFoundException : ScrumPokerException
{
    protected NotFoundException()
    {
        
    }

    protected NotFoundException(string message) : base(message)
    {
        
    }
}