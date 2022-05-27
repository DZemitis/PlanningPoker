using AutoMapper;
using Microsoft.Extensions.Logging;
using ScrumPoker.Common.Models;
using ScrumPoker.DataAccess.Interfaces;
using ScrumPoker.DataAccess.Models.EFContext;

namespace ScrumPoker.DataAccess.Data;

public class RoundRepository : IRoundRepository
{
    private readonly IMapper _mapper;
    private readonly IScrumPokerContext _context;
    private readonly ILogger<RoundRepository> _logger;

    public RoundRepository(IMapper mapper, IScrumPokerContext context, ILogger<RoundRepository> logger)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }
}