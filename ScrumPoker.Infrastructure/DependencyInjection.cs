﻿using Microsoft.Extensions.DependencyInjection;
using ScrumPoker.Business;
using ScrumPoker.Business.Interfaces.Interfaces;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Repositories;

namespace ScrumPoker.Infrastructure;

public static class DependencyInjection
{
    public static void Register(this IServiceCollection services)
    {
        services.AddTransient<IGameRoomService, GameRoomService>();
        services.AddTransient<IGameRoomRepository, GameRoomRepository>();
        services.AddTransient<IPlayerRepository, PlayerRepository>();
        services.AddTransient<IPlayerService, PlayerService>();
        services.AddTransient<IScrumPokerContext, ScrumPokerContext>();
        services.AddTransient<IVoteRegistrationRepository, VoteRegistrationRepository>();
        services.AddTransient<IRoundRepository, RoundRepository>();
        services.AddTransient<IRoundService, RoundService>();
        services.AddTransient<IVoteRegistrationService, VoteRegistrationService>();
    }
}