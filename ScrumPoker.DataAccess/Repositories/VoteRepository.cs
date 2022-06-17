using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.NotFoundExceptions;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Repositories;

/// <inheritdoc cref="IVoteRepository" />
public class VoteRepository : RepositoryBase, IVoteRepository
{
    public VoteRepository(IMapper mapper, IScrumPokerContext context,
        ILogger<VoteRepository> logger) :
        base(mapper, context, logger)
    {
    }

    public async Task<List<Vote>> GetListById(int id)
    {
        var voteDto = await Context.Votes.Where(x => x.RoundId == id).ToListAsync();
        var voteResponse = Mapper.Map<List<Vote>>(voteDto);

        return voteResponse;
    }

    public async Task<Vote> GetById(int id)
    {
        var voteDto = await GetVoteById(id);
        var voteResponse = Mapper.Map<Vote>(voteDto);

        return voteResponse;
    }

    public async Task<Vote> CreateOrUpdate(Vote voteRequest)
    {
        var roundDto = await GetRoundById(voteRequest.RoundId);
        var gameRoomDto = await GetGameRoomById(roundDto.GameRoomId);
        var playerDto = gameRoomDto.GameRoomPlayers.SingleOrDefault(gr => gr.PlayerId == voteRequest.PlayerId);
        if (playerDto == null)
        {
            Logger.LogWarning
            ("Player(ID{PlayerId}) in Game Room (ID{GameRoomId}) was not found",
                voteRequest.PlayerId, gameRoomDto.Id);
            throw new IdNotFoundException
                ($"{typeof(Player)} in game room {gameRoomDto.Id} with player ID {voteRequest.PlayerId} not found");
        }

        var vote =
            await Context.Votes.SingleOrDefaultAsync(x =>
                x.PlayerId == voteRequest.PlayerId && x.RoundId == voteRequest.RoundId);

        var voteDto = new VoteDto();
        if (vote == null)
        {
            voteDto.VoteResult = voteRequest.VoteResult;
            voteDto.PlayerId = voteRequest.PlayerId;
            voteDto.RoundId = voteRequest.RoundId;

            await Context.Votes.AddAsync(voteDto);
        }
        else
        {
            vote.VoteResult = voteRequest.VoteResult;
            voteDto = vote;
        }

        await Context.SaveChangesAsync();
        var voteResponse = Mapper.Map<Vote>(voteDto);

        return voteResponse;
    }

    public async Task ClearRoundVotes(int roundId)
    {
        var round = await GetRoundById(roundId);

        Context.Votes.RemoveRange(round.Votes);

        await Context.SaveChangesAsync();
    }
}