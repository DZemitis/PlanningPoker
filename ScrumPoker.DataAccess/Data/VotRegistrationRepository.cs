using AutoMapper;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data;

/// <inheritdoc cref="ScrumPoker.DataAccess.Interfaces.IVoteRegistrationRepository"/>
public class VoteRegistrationRepository : RepositoryBase, IVoteRegistrationRepository
{
    public VoteRegistrationRepository(IMapper mapper, IScrumPokerContext context, ILogger<RepositoryBase> logger) :
        base(mapper, context, logger)
    {
    }

    public List<VoteRegistration> GetListById(int id)
    {
        var voteRegistrationDto = _context.Votes.Where(x => x.RoundId == id).ToList();
        var voteRegistrationResponse = _mapper.Map<List<VoteRegistration>>(voteRegistrationDto);

        return voteRegistrationResponse;
    }

    public VoteRegistration GetById(int id)
    {
        var voteRequest = _context.Votes.SingleOrDefault(x => x.Id == id);
        var voteResponse = _mapper.Map<VoteRegistration>(voteRequest);

        return voteResponse;
    }

    public VoteRegistration CreateOrUpdate(VoteRegistration voteRequest)
    {
        var roundDto = RoundIdValidation(voteRequest.RoundId);
        var gameRoomDto = GameRoomIdValidation(roundDto.GameRoomId);
        PlayerIdValidationInGameRoom(voteRequest.PlayerId, gameRoomDto);

        var voteRegistrationDto = _context.Votes;
        var votingHistory = _context.Rounds.Select(x => x.Votes).First();
        
        var checkVote =
            voteRegistrationDto.SingleOrDefault(x =>
                x.PlayerId == voteRequest.PlayerId && x.RoundId == voteRequest.RoundId);
        
        var voteRequestDto = new VoteRegistrationDto();
        if (checkVote == null)
        {
            voteRequestDto.Vote = voteRequest.Vote;
            voteRequestDto.PlayerId = voteRequest.PlayerId;
            voteRequestDto.RoundId = voteRequest.RoundId;
            
            voteRegistrationDto.Add(voteRequestDto);
            votingHistory.Add(voteRequestDto);
        }
        else
        {
            checkVote.Vote = voteRequest.Vote;
            voteRequestDto = _mapper.Map<VoteRegistrationDto>(checkVote);
        }

        _context.SaveChanges();
        var voteRegistrationResponse = _mapper.Map<VoteRegistration>(voteRequestDto);

        return voteRegistrationResponse;
    }
    
    public void ClearRoundVotes(int roundId)
    {
        RoundIdValidation(roundId);
        var votesDto = _context.Votes;
        var votes = _context.Votes.Where(x => x.RoundId == roundId);

        votesDto.RemoveRange(votes);

        _context.SaveChanges();
    }
}