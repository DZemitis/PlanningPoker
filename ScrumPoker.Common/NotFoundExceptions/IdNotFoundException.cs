namespace ScrumPoker.Common.NotFoundExceptions;

public class IdNotFoundException : NotFoundException
{
    public IdNotFoundException() : base("ID not found")
    {
        
    }
}