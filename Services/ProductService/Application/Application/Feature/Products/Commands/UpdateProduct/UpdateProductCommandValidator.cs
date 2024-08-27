﻿using FluentValidation;

namespace Application.Feature.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Product ID cannot be empty.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.CategoryIds)
                .NotEmpty()
                .WithMessage("At least one category must be selected.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID cannot be empty.");
        }
    }
}
