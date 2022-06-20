using FluentValidation;
using ScrumPoker.Web.Models.Models.WebRequest;

namespace ScrumPoker.Web.Validators;

public class UpdateGameRoomApiRequestValidator : AbstractValidator<UpdateGameRoomApiRequest>
{
    public UpdateGameRoomApiRequestValidator()
    {
        RuleFor(g => g.Name)
            .NotEmpty()
            .WithMessage("Name of the game room cannot be empty")
            .Matches("^[a-zA-Z0-9 ]*$")
            .WithMessage("Name of game room can only contain numbers and letters")
            .Length(3, 20)
            .WithMessage("Name of the game room must contain at least 3 characters and no more than 20 characters");
    }
}