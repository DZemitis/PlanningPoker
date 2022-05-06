namespace ScrumPoker.Common.ConflictExceptions;

public class IdAlreadyExistException : ConflictException
{
    public IdAlreadyExistException()
    {
        
    }
    
    public IdAlreadyExistException(string message) : base(message)
    {
    }
}