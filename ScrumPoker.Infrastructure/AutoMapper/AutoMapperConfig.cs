using AutoMapper;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAcces.Models.Models;
using ScrumPoker.Web.Models.Models.WebRequest;
using ScrumPoker.Web.Models.Models.WebResponse;

namespace ScrumPoker.Infrastructure.AutoMapper;

public class AutoMapperConfig
{
    public static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<GameRoom, CreateGameRoomRequest>();
            cfg.CreateMap<CreateGameRoomRequest, GameRoom>()
                .ForMember(dest=>dest.Id,
                    opt =>
                        opt.Ignore());
            
            cfg.CreateMap<UpdateGameRoomRequest, GameRoom>();
            cfg.CreateMap<GameRoom, GameRoomDto>();
            cfg.CreateMap<GameRoom, GameRoomResponse>();
            cfg.CreateMap<GameRoomDto, GameRoom>();
        });

        //only during development, validate your mapping s; remove it before release
        configuration.AssertConfigurationIsValid();

        var mapper = configuration.CreateMapper();
        return mapper;
    }
}