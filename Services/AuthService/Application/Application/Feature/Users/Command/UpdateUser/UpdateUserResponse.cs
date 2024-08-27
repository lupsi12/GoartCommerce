using System;
using System.Collections.Generic;
using Enum;

namespace Application.Feature.Users.Commands.UpdateUsers
{
    public class UpdateUserResponse
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone  { get; set; }
        public string Password { get; set; } 
        public DateTime BirthDate { get; set; } 
        public Status Status { get; set; }
    }
}