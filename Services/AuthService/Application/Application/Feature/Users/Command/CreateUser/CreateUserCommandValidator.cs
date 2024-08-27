using Application.Feature.Users.Commands.CreateUser;
using FluentValidation;

namespace Application.Feature.Users.Commands.CreatUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Title cannot be empty.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty.");

        }
    }
}
