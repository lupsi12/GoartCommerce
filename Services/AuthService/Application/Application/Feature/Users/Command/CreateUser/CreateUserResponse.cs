using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enum;

namespace Application.Feature.Users.Commands.CreateUser
{
    public class CreateUserResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; } 
        public DateTime BirthDate { get; set; } 
        public string Phone { get; set; }
        public Roles Role { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
