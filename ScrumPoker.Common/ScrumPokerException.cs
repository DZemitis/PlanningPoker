namespace ScrumPoker.Common;

public class ScrumPokerException : Exception
{ 
    public new string? Message { get; protected init; }

    protected ScrumPokerException()
    {
        
    }
}