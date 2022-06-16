namespace ScrumPoker.Common.ConflictExceptions;

public class IdAlreadyExistException : ConflictException
{
    public IdAlreadyExistException(string message)
    {
        Message = message;
    }
}