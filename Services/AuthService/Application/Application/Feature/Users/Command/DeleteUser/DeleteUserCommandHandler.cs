using Application.Feature.Users.DeleteUser;
using Core.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feature.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, DeleteUserResponse>
    {
        private readonly IReadRepository<User> _userReadRepository;
        private readonly IWriteRepository<User> _userWriteRepository;

        public DeleteUserCommandHandler(
            IReadRepository<User> userReadRepository,
            IWriteRepository<User> userWriteRepository)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userReadRepository.GetAsync(p => p.Id == request.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            await _userWriteRepository.HardDeleteAsync(user);
            await _userWriteRepository.SaveAsync();

            return new DeleteUserResponse
            {
                UserId = user.Id,
                Name = user.Name,
                IsDeleted = true
            };
        }
    }
}