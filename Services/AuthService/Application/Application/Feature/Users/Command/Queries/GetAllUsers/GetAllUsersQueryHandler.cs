using Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Feature.Users.Queries.GetAllUsers;
using Domain.Entities;

namespace Application.Feature.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, List<GetAllUsersResponse>>
    {
        private readonly IReadRepository<User> _userReadRepository;

        public GetAllUsersQueryHandler(IReadRepository<User> userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<List<GetAllUsersResponse>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userReadRepository.GetAllByPagingAsync(
                enableTracking: false,
                currentPage: request.PageNumber,
                pageSize: request.PageSize);

            return users.Select(user => new GetAllUsersResponse
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
            }).ToList();
        }
    }
}