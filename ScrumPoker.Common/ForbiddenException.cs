namespace ScrumPoker.Common;

public class ForbiddenException : ScrumPokerException
{
    protected ForbiddenException()
    {
    }
    protected ForbiddenException(string message) : base(message)
    {
    }
}
