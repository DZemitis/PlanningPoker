namespace ScrumPoker.Business.Interfaces.Interfaces;

public interface IAuthService
{
    string GenerateToken(int id);
}