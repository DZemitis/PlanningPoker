﻿using AutoMapper;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Models.Models;
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
        CreateMap<GameRoom, GameRoomApiResponse>()
            .ForMember(dest=>dest.MasterId, opt=>
                opt.MapFrom(x=>x.MasterId));
        CreateMap<GameRoom, GameRoomInPlayerListApiResponse>();
        CreateMap<GameRoomDto, GameRoom>()
            .ForMember(dest => dest.Players, opt =>
                opt.MapFrom(src => src.GameRoomPlayers.Select(x => x.Player)))
            .ForMember(dest=>dest.MasterId, opt=>
                opt.MapFrom(x=>x.Master.Id))
            .ForMember(x=>x.CurrentRoundId, opt=>
                opt.MapFrom(src=>src.CurrentRound.RoundId));
        CreateMap<CreatePlayerApiRequest, Player>();
        CreateMap<UpdatePlayerApiRequest, Player>();
        CreateMap<Player, PlayerDto>();
        CreateMap<Player, PlayerApiResponse>();
        CreateMap<Player, PlayerInGameRoomApiResponse>();
        CreateMap<PlayerDto, Player>()
            .ForMember(dest=>dest.GameRooms, opt=>
                opt.MapFrom(pd=>pd.PlayerGameRooms.Select(x=>x.GameRoom)));
    }
}