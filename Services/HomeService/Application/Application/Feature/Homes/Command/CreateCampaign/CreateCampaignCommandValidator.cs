using FluentValidation;

namespace Application.Feature.Homes.Command.CreateCampaign
{
    public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommandRequest>
    {
        public CreateCampaignCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty.");

        }
    }
}
