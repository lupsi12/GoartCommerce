using Application.Features.Users.Rules;
using Core.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feature.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserResponse>
    {
        private readonly UserRules _userRules;
        private readonly IReadRepository<User> _userReadRepository;
        private readonly IWriteRepository<User> _userWriteRepository;
        public CreateUserCommandHandler(
            UserRules userRules,
            IReadRepository<User> userReadRepository,
            IWriteRepository<User> userWriteRepository)
        {
            _userRules = userRules;
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            UserRules.ValidateEmail(request.Email);
            await _userRules.ValidateEmailAsync(request.Email);
            User user = new User
            {
                Name = request.Name,
                LastName = request.LastName,
                Role = request.Role,
                Email = request.Email,
                Password = request.Password,
                Phone = request.Phone,
                BirthDate = request.BirthDate,
                Status = 0,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            await _userWriteRepository.AddAsync(user);
            await _userWriteRepository.SaveAsync(); 

            return new CreateUserResponse
            {
                UserId = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Password = user.Password,
                Phone = user.Phone,
                Role = user.Role,
                Status = user.Status,
                CreatedDate = user.CreatedDate
            };
        }

    }
}