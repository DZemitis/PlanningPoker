using Microsoft.AspNetCore.Http;
using ScrumPoker.Business.Interfaces.Interfaces;

namespace ScrumPoker.Business;

public class UserManager : IUserManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserManager(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetCurrentUserId()
    {
        var currentUserId = _httpContextAccessor.HttpContext!.User.Claims.Single(x => x.Type == "userId").Value;

        return int.Parse(currentUserId);
    }
}