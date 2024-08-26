using Domain.Common;
using Repository.Common;
using Repository.User;
using Shared.DomainObject.User;

namespace Domain.User;

public class UserService(IUserRepo userRepo ) :IUserService
{
    public async Task<List<GetUserDomainObject>> GetUsersAsync()
    {
        return await userRepo.GetUsersAsync();
    }

    public async Task<bool> CreateUserAsync(CreateUserDomainObject request)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateUserAsync(UpdateUserDomainObject request)
    {
        throw new NotImplementedException();
    }
}