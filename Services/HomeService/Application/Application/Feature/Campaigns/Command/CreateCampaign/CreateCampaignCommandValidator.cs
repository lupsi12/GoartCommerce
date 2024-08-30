using FluentValidation;

namespace Application.Feature.Campaigns.Command.CreateCampaign
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
