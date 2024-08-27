using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Features.Users.Rules
{
    public class UserRules
    {
        public void ValidateEmail(string email)
{
    if (string.IsNullOrWhiteSpace(email))
    {
        throw new ValidationException("Email cannot be null or empty.");
    }

    if (!email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
    {
        throw new ValidationException("The email must be a Gmail address.");
    }
}

    }
}