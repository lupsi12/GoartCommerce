using Application.Feature.Users.Commands.UpdateUser;
using Application.Feature.Users.Commands.UpdateUsers;
using Application.Features.Users.Rules;
using Core.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feature.Users.Commands.UpdateProduct
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserResponse>
    {
        private readonly IReadRepository<User> _userReadRepository;
        private readonly IWriteRepository<User> _userWriteRepository;

        public UpdateUserCommandHandler(
            IReadRepository<User> userReadRepository,
            IWriteRepository<User> userWriteRepository)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }
        public async Task<UpdateUserResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {

            var user = await _userReadRepository.GetAsync(p => p.Id == request.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            
                user.Name = request.Name;
                user.LastName = request.LastName;
                user.Password = request.Password;
                user.Phone = request.Phone;
                user.BirthDate = request.BirthDate;
                user.Status = request.Status;
            

            await _userWriteRepository.UpdateAsync(user);
            await _userWriteRepository.SaveAsync(); 

            return new UpdateUserResponse
            {
                Name = user.Name,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Password = user.Password,
                Phone = user.Phone,
                Status = user.Status
            };
        }
    }
}