using ScrumPoker.Business.Models.Models;

namespace ScrumPoker.DataAccess.Interfaces;

public interface IRoundRepository
{
    /// <summary>
    /// Returns specific round with votes
    /// </summary>
    /// <param name="id">Round ID</param>
    /// <returns>Round</returns>
    Round GetById(int id);

    /// <summary>
    /// Creates a round
    /// </summary>
    /// <param name="roundRequest">Game room ID and round description</param>
    /// <returns>Created round</returns>
    Round Create(Round roundRequest);

    /// <summary>
    /// Request to set specific state of the round
    /// </summary>
    /// <param name="roundRequest">Set round request</param>
    /// <returns>Updated Round</returns>
    void SetState(Round roundRequest);

    /// <summary>
    /// Request to update round description
    /// </summary>
    /// <param name="roundRequest">Round update request</param>
    /// <returns>Updated Round</returns>
    void Update(Round roundRequest);
}