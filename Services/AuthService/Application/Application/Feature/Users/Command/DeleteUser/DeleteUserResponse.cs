using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Users.Commands.DeleteUser
{

    public class DeleteUserResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}