namespace ScrumPoker.Business.Interfaces.Interfaces;

public interface IJwtService
{
    string CreateToken(int id);

}