using Application.Feature.Carts.Commands.UpdateCart;
using FluentValidation;

namespace Application.Features.Carts.Commands.UpdateCart
{
    public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommandRequest>
    {
        public UpdateCartCommandValidator()
        {
            RuleFor(x => x.CartId).GreaterThan(0).WithMessage("Cart ID must be greater than zero.");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("User ID must be greater than zero.");
            RuleForEach(x => x.Items).SetValidator(new CartItemUpdateDtoValidator());
        }

        public class CartItemUpdateDtoValidator : AbstractValidator<CartItemUpdateDto>
        {
            public CartItemUpdateDtoValidator()
            {
                RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Product ID must be greater than zero.");
                RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");
            }
        }
    }
}
