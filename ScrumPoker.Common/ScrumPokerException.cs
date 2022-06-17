namespace ScrumPoker.Common;

public class ScrumPokerException : Exception
{
    protected ScrumPokerException()
    {
    }

    public new string? Message { get; protected init; }
}