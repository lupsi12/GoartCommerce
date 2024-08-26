namespace Application.Messages.Dto.User.Response;

public class UserResponseDto(int id, string name, string lastname)
{
    public int Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string LastName { get; private set; } = lastname;
}