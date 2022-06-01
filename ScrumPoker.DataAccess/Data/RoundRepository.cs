using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data;

public class RoundRepository : IRoundRepository
{
    private readonly IScrumPokerContext _context;
    private readonly IValidation _validator;

    public RoundRepository(IScrumPokerContext context, IValidation validator)
    {
        _context = context;
        _validator = validator;
    }
    
    public void SetRoundState(RoundDto round)
    {
        var gameRoomDto = _validator.GameRoomIdValidation(round.GameRoomId);
        gameRoomDto.RoundDto.RoundState = round.RoundState;

        _context.SaveChanges();
    }
    
    public void SetRound(RoundDto round)
    {
        var gameRoomDto = _validator.GameRoomIdValidation(round.GameRoomId);
        gameRoomDto.CurrentRoundId = round.RoundId;

        _context.SaveChanges();
    }
}