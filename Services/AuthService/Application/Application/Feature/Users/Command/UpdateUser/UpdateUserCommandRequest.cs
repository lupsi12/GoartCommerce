using Application.Feature.Users.Commands.UpdateUsers;
using Enum;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel;

namespace Application.Feature.Users.Commands.UpdateUser
{
    public class UpdateUserCommandRequest : IRequest<UpdateUserResponse>
    {
        [DefaultValue(1)]
        public int UserId { get; set; }

        [DefaultValue("Default User Name")]
        public string Name { get; set; }
        [DefaultValue("Default User LastName")] 
        public string LastName { get; set; }
        [DefaultValue("password")] 
        public string Password { get; set; } 
        [DefaultValue("1900-01-01")] 
        public DateTime BirthDate { get; set; }

        [DefaultValue("000-000-0000")]
        public string Phone { get; set; }
        [DefaultValue(0)]
        public Status Status { get; set; }
    }
}