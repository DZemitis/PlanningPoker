using Microsoft.AspNetCore.Mvc.Filters;

namespace ScrumPoker.Common;

public class ScrumPokerException : Exception
{
    public ScrumPokerException(int statusCode, string message) =>
        (StatusCode, Message) = (statusCode, message);

    public int StatusCode { get; set; }

    public object? Value { get; set; }
    
    public new string Message { get; set; }

    protected ScrumPokerException()
    {
        
    }

    protected ScrumPokerException(string message) : base(message)
    {
        
    }
}