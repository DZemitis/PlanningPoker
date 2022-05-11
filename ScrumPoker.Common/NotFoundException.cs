namespace ScrumPoker.Common;

public class NotFoundException : ScrumPokerException
{
    public NotFoundException(string message) =>
        (Message) = (message);
    
    protected NotFoundException()
    {
        
    }
}