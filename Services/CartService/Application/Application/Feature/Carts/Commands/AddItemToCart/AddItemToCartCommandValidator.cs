using FluentValidation;

namespace Application.Features.Carts.Commands.AddItemToCart
{
    public class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommandRequest>
    {
        public AddItemToCartCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("User ID must be greater than zero.");
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Product ID must be greater than zero.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}
