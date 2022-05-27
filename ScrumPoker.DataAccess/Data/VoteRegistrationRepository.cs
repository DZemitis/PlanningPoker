using AutoMapper;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;

namespace ScrumPoker.DataAccess.Data;

public class VoteRegistrationRepository : IVoteRegistrationRepository
{
    private readonly IMapper _mapper;
    private readonly IScrumPokerContext _context;
    private readonly ILogger<VoteRegistrationRepository> _logger;
    private readonly IValidation _validator;

    public VoteRegistrationRepository(IMapper mapper, IScrumPokerContext context, ILogger<VoteRegistrationRepository> logger, IValidation validator)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
        _validator = validator;
    }
    
    public void ClearRoundVotes(GameRoom gameRoom)
    {
        var gameRoomDto = _validator.GameRoomIdValidation(gameRoom.Id);
        var votesDto = _context.Votes;
        var votes = _context.Votes.Where(x=>x.GameRoomId == gameRoom.Id);
        
        votesDto.RemoveRange(votes);

        _context.SaveChanges();
    }
}