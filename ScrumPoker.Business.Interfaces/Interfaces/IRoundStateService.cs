using ScrumPoker.Business.Models.Models;
using ScrumPoker.Common.Models;

namespace ScrumPoker.Business.Interfaces.Interfaces;

public interface IRoundStateService
{
    void ValidateRoundState(RoundState roundRequest, RoundState roundDto);
}