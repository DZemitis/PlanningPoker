using Microsoft.Extensions.DependencyInjection;
using ScrumPoker.Business;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.DataAccess.Data;
using ScrumPoker.DataAccess.Interfaces;

namespace ScrumPoker.Infrastructure;

public static class DependencyInjection
{
    public static void Register(this IServiceCollection services)
    {
        services.AddTransient<IGameRoomService, GameRoomService>();
        services.AddTransient<IGameRoomRepository, GameRoomRepository>();
        services.AddTransient<IPlayerRepository, PlayerRepository>();
        services.AddTransient<IPlayerService, PlayerService>();
    }
}