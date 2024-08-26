using Application.Messages.Dto.User.Response;
using Application.Messages.Query.User;
using Domain.Common;
using Core.Cqs;

namespace Application.QueryHandler.User;

internal class UserQueryHandler(IUserService userService) : IQueryHandler<UserQuery, IEnumerable<UserResponseDto>>
{
    public async Task<IEnumerable<UserResponseDto>> Handle(UserQuery request, CancellationToken cancellationToken)
    {
        var userDomainObject = await userService.GetUsersAsync();
        return userDomainObject.Select(x => new UserResponseDto(x.Id, x.Name, x.LastName)).ToList();
    }
}