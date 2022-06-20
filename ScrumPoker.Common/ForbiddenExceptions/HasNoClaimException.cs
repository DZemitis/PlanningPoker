namespace ScrumPoker.Common.ForbiddenExceptions;

public class HasNoClaimException : ForbiddenException
{
    public HasNoClaimException(string message)
    {
        Message = message;
    }
}