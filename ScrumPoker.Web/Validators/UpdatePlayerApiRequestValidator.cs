using FluentValidation;
using ScrumPoker.Web.Models.Models.WebRequest;

namespace ScrumPoker.Web.Validators;

public class UpdatePlayerApiRequestValidator : AbstractValidator<UpdatePlayerApiRequest>
{
    public UpdatePlayerApiRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty")
            .Matches("^[a-zA-Z ]*$")
            .WithMessage("Name can only contain letters")
            .Length(3, 20)
            .WithMessage("Name must contain least 3 characters and no more than 20 characters");

        RuleFor(p => p.Email)
            .EmailAddress()
            .WithMessage("Email must be valid")
            .NotEmpty()
            .WithMessage("Email cannot be empty");
    }
}