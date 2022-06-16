namespace ScrumPoker.Common.NotFoundExceptions;

public class IdNotFoundException : NotFoundException
{
    public IdNotFoundException(string message)
    {
        Message = message;
    }
}