using Shared.DomainObject.User;

namespace Domain.Common;

public interface IUserService
{
    Task<List<GetUserDomainObject>> GetUsersAsync();
    Task<bool> CreateUserAsync(CreateUserDomainObject request);
    Task<bool> UpdateUserAsync(UpdateUserDomainObject request);

}