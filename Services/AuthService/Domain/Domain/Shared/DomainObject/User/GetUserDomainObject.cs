namespace Shared.DomainObject.User;

public class GetUserDomainObject(int id, string name, string lastname)
{
    public int Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string LastName { get; private set; } = lastname;
}