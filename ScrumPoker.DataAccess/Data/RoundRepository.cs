using AutoMapper;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;

namespace ScrumPoker.DataAccess.Data;

public class RoundRepository : IRoundRepository
{
    private readonly IMapper _mapper;
    private readonly IScrumPokerContext _context;
    private readonly ILogger<RoundRepository> _logger;
    private readonly IValidation _validator;

    public RoundRepository(IMapper mapper, IScrumPokerContext context, ILogger<RoundRepository> logger, IValidation validator)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
        _validator = validator;
    }
    
    public void SetRoundState(Round round)
    {
        var gameRoomDto = _validator.GameRoomIdValidation(round.GameRoomId);
        gameRoomDto.CurrentRound.RoundState = round.RoundState;

        _context.SaveChanges();
    }
    
    public void SetRound(Round round)
    {
        var gameRoomDto = _validator.GameRoomIdValidation(round.GameRoomId);
        gameRoomDto.CurrentRoundId = round.RoundId;

        _context.SaveChanges();
    }
}