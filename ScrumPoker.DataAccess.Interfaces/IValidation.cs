using ScrumPoker.Business.Models.Models;
using ScrumPoker.DataAccess.Models.Models;

namespace ScrumPoker.DataAccess.Interfaces;

public interface IValidation
{
    GameRoomDto GameRoomIdValidation(int gameRoomId);
    PlayerDto PlayerIdValidation(int playerId);
    GameRoomPlayer PlayerIdValidationInGameRoom(int playerId, GameRoomDto gameRoomDto);

    void ValidateAlreadyExistException(GameRoom gameRoomRequest);
}