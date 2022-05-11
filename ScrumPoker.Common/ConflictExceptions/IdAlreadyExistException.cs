namespace ScrumPoker.Common.ConflictExceptions;

public class IdAlreadyExistException : ConflictException
{
    public IdAlreadyExistException(int statusCode, string message) =>
        (StatusCode, Message) = (statusCode, message);
    
    public IdAlreadyExistException(string message) : base(message)
    {
    }
}