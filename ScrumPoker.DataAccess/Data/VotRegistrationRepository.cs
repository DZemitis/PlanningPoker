using AutoMapper;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Data;

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

    public VoteRegistration Create(VoteRegistration voteRequest)
    {
        var roundDto = RoundIdValidation(voteRequest.RoundId);
        var gameRoomDto = GameRoomIdValidation(roundDto.GameRoomId);
        PlayerIdValidationInGameRoom(voteRequest.PlayerId, gameRoomDto);

        var voteRegistrationDto = _context.Votes;
        var votingHistory = _context.Rounds.Select(x => x.Votes).First();
        VoteAlreadyMadeValidation(voteRequest, voteRegistrationDto);

        var voteRequestDto = new VoteRegistrationDto
        {
            Vote = voteRequest.Vote,
            PlayerId = voteRequest.PlayerId,
            RoundId = voteRequest.RoundId
        };

        voteRegistrationDto.Add(voteRequestDto);
        votingHistory.Add(voteRequestDto);
        _context.SaveChanges();

        var voteRegistrationResponse = _mapper.Map<VoteRegistration>(voteRequestDto);

        return voteRegistrationResponse;
    }

    public void Update(VoteRegistration vote)
    {
        var voteRequest = _mapper.Map<VoteRegistrationDto>(vote);
        var voteRegistrationDto = VoteNotFoundValidation(voteRequest);

        voteRegistrationDto.Vote = vote.Vote;

        _context.SaveChanges();
    }

    public void ClearRoundVotes(int roundId)
    {
        var votesDto = _context.Votes;
        var votes = _context.Votes.Where(x => x.RoundId == roundId);

        votesDto.RemoveRange(votes);

        _context.SaveChanges();
    }
}