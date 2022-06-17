namespace ScrumPoker.Common.ConflictExceptions;

public class VoteAlreadyExistException : ConflictException
{
    public VoteAlreadyExistException(string message)
    {
        Message = message;
    }
}