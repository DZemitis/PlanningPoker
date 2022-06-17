using FluentValidation;
using ScrumPoker.Web.Models.Models.WebRequest;

namespace ScrumPoker.Web.Validators;

public class UpdateRoundApiRequestValidator : AbstractValidator<UpdateRoundApiRequest>
{
    public UpdateRoundApiRequestValidator()
    {
        RuleFor(r => r.RoundState)
            .IsInEnum()
            .WithMessage("Round has 3 states, value in between 1 and 3 is required");
    }
}