using Database.Context;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using Shared.DomainObject.User;

namespace Repository.User;

public class UserRepo(IAuthDbContext context) : IUserRepo
{
    public async Task<List<GetUserDomainObject>> GetUsersAsync()
    {
        return await context.Users.Select(x => new GetUserDomainObject(x.Id, x.Name, x.LastName))
            .ToListAsync();
    }

    public Task<bool> CreateUserAsync(CreateUserDomainObject request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateUserAsync(UpdateUserDomainObject request)
    {
        throw new NotImplementedException();
    }
}