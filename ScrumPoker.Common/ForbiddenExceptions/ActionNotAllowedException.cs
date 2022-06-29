namespace ScrumPoker.Common.ForbiddenExceptions;

public class ActionNotAllowedException : ForbiddenException
{
    public ActionNotAllowedException()
    {
    }
    public ActionNotAllowedException(string message) : base(message)
    {
    }
}