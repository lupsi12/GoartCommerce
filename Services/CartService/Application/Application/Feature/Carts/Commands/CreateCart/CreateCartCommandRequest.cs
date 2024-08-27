using MediatR;
using System.ComponentModel;

namespace Application.Feature.Carts.Commands.CreateCart
{
    public class CreateCartCommandRequest : IRequest<CreateCartCommandResponse>
    {
        [DefaultValue(1)]
        public int UserId { get; set; }
    }
}
