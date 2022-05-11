using Microsoft.AspNetCore.Mvc.Filters;

namespace ScrumPoker.Common;

public class ScrumPokerException : Exception
{
    public ScrumPokerException(string message) =>
        (Message) = (message);
    public new string? Message { get; set; }

    protected ScrumPokerException()
    {
        
    }
}