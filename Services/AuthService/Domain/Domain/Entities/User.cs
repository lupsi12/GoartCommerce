using Enum;
using Core.Shared.EntityBase;

namespace Domain.Entities;

public class User : EntityBase
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; } 
    public string Password { get; set; } 
    public DateTime BirthDate { get; set; } 
    public string Phone { get; set; }
    public Roles Role { get; set; }
    public Status Status { get; set; }
}