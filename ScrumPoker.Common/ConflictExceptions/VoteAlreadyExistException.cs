namespace ScrumPoker.Common.ConflictExceptions;

public class VoteAlreadyExistException : ConflictException
{
    public VoteAlreadyExistException()
    {
    }
    public VoteAlreadyExistException(string message) : base(message)
    {
    }
}