using Core.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feature.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, GetUserByIdResponse>
    {
        private readonly IReadRepository<User> _userReadRepository;

        public GetUserByIdQueryHandler(IReadRepository<User> userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<GetUserByIdResponse> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userReadRepository.GetAsync(
                predicate: p => p.Id == request.UserId,
                enableTracking: false);

            if (user == null)
            {
                return null; 
            }
            return new GetUserByIdResponse
            {
                Id = user.Id,
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