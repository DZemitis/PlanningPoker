using Microsoft.Extensions.DependencyInjection;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.Data.Data;
using ScrumPoker.DataBase.Interfaces;
using ScrumPoker.Services;

namespace ScrumPoker.Infrastructure;

public static class DependencyInjection
{
    public static void Register(this IServiceCollection a)
    {
        a.AddTransient<IGameRoomService, GameRoomService>();
        a.AddTransient<IGameRoomRepository, GameRoomRepository>();
    }
}