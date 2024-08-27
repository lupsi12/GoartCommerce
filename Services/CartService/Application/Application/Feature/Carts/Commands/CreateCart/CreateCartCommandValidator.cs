using FluentValidation;

namespace Application.Feature.Carts.Commands.CreateCart
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommandRequest>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("User ID must be greater than zero.");
        }
    }
}
