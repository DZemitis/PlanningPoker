using AutoMapper;
using Microsoft.Extensions.Logging;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;

namespace ScrumPoker.DataAccess.Data;

public class VoteReviewRepository : IVoteReviewRepository
{
    private readonly IMapper _mapper;
    private readonly IScrumPokerContext _context;
    private readonly ILogger<VoteReviewRepository> _logger;

    public VoteReviewRepository(IMapper mapper, IScrumPokerContext context, ILogger<VoteReviewRepository> logger)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }
}