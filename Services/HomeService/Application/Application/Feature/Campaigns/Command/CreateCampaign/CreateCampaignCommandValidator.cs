using Application.Feature.Users.Commands.CreateUser;
using FluentValidation;

namespace Application.Feature.Users.Commands.CreatUser
{
    public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommandRequest>
    {
        public CreateCampaignCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Title cannot be empty.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Email cannot be empty.");

        }
    }
}
