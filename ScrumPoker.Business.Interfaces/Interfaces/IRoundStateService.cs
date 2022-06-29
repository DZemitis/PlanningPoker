using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.Business.Interfaces.Interfaces;

public interface IRoundStateService
{
    void ValidateRoundState(Round roundRequest, Round roundDto);
}