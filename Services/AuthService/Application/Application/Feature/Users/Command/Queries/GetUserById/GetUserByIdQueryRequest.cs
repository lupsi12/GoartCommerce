using System;
using System.Collections.Generic;
using Enum;
using MediatR;

namespace Application.Feature.Users.Queries.GetUserById
{
    public class GetUserByIdQueryRequest : IRequest<GetUserByIdResponse>
    {
        public int UserId { get; set; }
    }
}