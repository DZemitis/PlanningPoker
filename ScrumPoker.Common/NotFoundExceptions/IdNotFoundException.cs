namespace ScrumPoker.Common.NotFoundExceptions;

public class IdNotFoundException : NotFoundException
{
    public IdNotFoundException(int statusCode, string message) =>
        (StatusCode, Value) = (statusCode, message);
    public IdNotFoundException(string message) : base(message)
    {
        
    }
}