namespace ScrumPoker.Common.NotFoundExceptions;

public class IdNotFoundException : NotFoundException
{
    public IdNotFoundException()
    {
    }
    public IdNotFoundException(string message) : base(message)
    {
    }
}