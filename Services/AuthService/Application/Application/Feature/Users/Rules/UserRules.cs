using Core.Repositories;
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
        private readonly IReadRepository<User> _userReadRepository;

        public UserRules(IReadRepository<User> userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }
        public static void ValidateEmail(string email)
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
public async Task ValidateEmailAsync(string email)
        {
            // E-posta adresinin geçerli olup olmadığını kontrol et
            ValidateEmail(email);

            // E-posta adresinin benzersizliğini kontrol et
            bool emailExists = (await _userReadRepository.GetAllAsync())
                .Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (emailExists)
            {
                throw new Exception("The email address is already in use.");
            }
        }

    }
    
}