namespace ScrumPoker.Common.ConflictExceptions;

public class IdAlreadyExistException : ConflictException
{
    public IdAlreadyExistException() : base ("ID already exists")
    {
        
    }
    
    public IdAlreadyExistException(string message) : base(message)
    {
    }
}