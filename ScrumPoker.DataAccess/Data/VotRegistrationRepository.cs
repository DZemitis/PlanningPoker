using AutoMapper;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.Models;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

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

    public void CreateVote(VoteRegistration voteRequest)
    {
        var gameRoomDto = _validator.GameRoomIdValidation(voteRequest.GameRoomId);
        _validator.PlayerIdValidationInGameRoom(voteRequest.PlayerId, gameRoomDto);
        var voteRegistrationDto = _context.Votes;
        var votingHistory = _context.Rounds.Select(x => x.Votes).First();

        var voteRequestDto = new VoteRegistrationDto
        {
            Vote = voteRequest.Vote,
            PlayerId = voteRequest.PlayerId,
            GameRoomId = voteRequest.GameRoomId,
            RoundId = voteRequest.RoundId
        };
        
        var expectedRoundState = gameRoomDto.CurrentRound.RoundState;
        if (expectedRoundState.Equals((RoundState) 2))
        {
            voteRegistrationDto.Add(voteRequestDto);
            votingHistory.Add(voteRequestDto);
        }

        _context.SaveChanges();
    }

    public void ClearRoundVotes(VoteRegistrationDto vote)
    {
        _validator.GameRoomIdValidation(vote.GameRoomId);
        var votesDto = _context.Votes;
        var votes = _context.Votes.Where(x=>x.GameRoomId == vote.GameRoomId);
        
        votesDto.RemoveRange(votes);

        _context.SaveChanges();
    }
}