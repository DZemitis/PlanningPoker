using Microsoft.AspNetCore.Mvc.Filters;

namespace ScrumPoker.Common;

public class ScrumPokerException : Exception
{
    public ScrumPokerException(int statusCode, object? value = null) =>
        (StatusCode, Value) = (statusCode, value);

    public int StatusCode { get; set; }

    public object? Value { get; set; }

    protected ScrumPokerException()
    {
        
    }

    protected ScrumPokerException(string message) : base(message)
    {
        
    }
}