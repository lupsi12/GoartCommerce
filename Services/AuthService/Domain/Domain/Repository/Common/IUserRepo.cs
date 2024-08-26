using Shared.DomainObject.User;

namespace Repository.Common;

public interface IUserRepo
{
    Task<List<GetUserDomainObject>> GetUsersAsync();
    Task<bool> CreateUserAsync(CreateUserDomainObject request);
    Task<bool> UpdateUserAsync(UpdateUserDomainObject request);
}