﻿using AutoMapper;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAcces.Models.Models;
using ScrumPoker.Web.Models.Models.WebRequest;
using ScrumPoker.Web.Models.Models.WebResponse;

namespace ScrumPoker.Infrastructure.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateGameRoomApiRequest, GameRoom>()
            .ForMember(dest => dest.Id,
                opt =>
                    opt.Ignore());
        CreateMap<UpdateGameRoomApiRequest, GameRoom>();
        CreateMap<GameRoom, GameRoomDto>();
        CreateMap<GameRoom, GameRoomApiResponse>();
        CreateMap<GameRoomDto, GameRoom>();
        CreateMap<CreatePlayerApiRequest, Player>();
        CreateMap<UpdatePlayerApiRequest, Player>();
        CreateMap<Player, PlayerDto>();
        CreateMap<Player, PlayerApiResponse>();
        CreateMap<PlayerDto, Player>();
    }
}