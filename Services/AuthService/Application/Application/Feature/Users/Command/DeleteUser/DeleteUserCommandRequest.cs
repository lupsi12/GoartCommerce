using System;
using System.Collections.Generic;
using Application.Feature.Users.Commands.DeleteUser;
using Enum;
using MediatR;

namespace Application.Feature.Users.DeleteUser
{
    public class DeleteUserCommandRequest : IRequest<DeleteUserResponse>
    {
        public int UserId { get; set; }
    }
}