namespace ScrumPoker.Business.Interfaces.Interfaces;

public interface IUserManager
{
    int GetCurrentUserId();
    string CreateToken(int id);
}