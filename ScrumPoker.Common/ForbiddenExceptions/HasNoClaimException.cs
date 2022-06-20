namespace ScrumPoker.Common.ForbiddenExceptions;

public class ActionNotAllowedException : ForbiddenException
{
    public ActionNotAllowedException(string message)
    {
        Message = message;
    }
}