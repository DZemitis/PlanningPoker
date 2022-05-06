namespace ScrumPoker.Common;

public class ScrumPokerException : Exception
{
    public ScrumPokerException() : base("Exception")
    {
        
    }

    protected ScrumPokerException(string message) : base(message)
    {
        
    }
}