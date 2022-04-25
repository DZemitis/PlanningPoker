using Microsoft.Extensions.DependencyInjection;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Data.Data;
using ScrumPoker.DataBase.Interfaces;
using ScrumPoker.Services;

namespace ScrumPoker.Infrastructure;

public static class DependencyInjection
{
    public static void Register(this IServiceCollection services)
    {
        services.AddTransient<IGameRoomService, GameRoomService>();
        services.AddTransient<IGameRoomRepository, GameRoomRepository>();
    }
}