namespace ScrumPoker.Common.ConflictExceptions;

public class HasNoClaimException : ConflictException
{
    public HasNoClaimException(string message)
    {
        Message = message;
    }
}