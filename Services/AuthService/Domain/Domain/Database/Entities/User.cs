using Database.Base;
using Enums;

namespace Database.Entities;

public class User : BaseEntity
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