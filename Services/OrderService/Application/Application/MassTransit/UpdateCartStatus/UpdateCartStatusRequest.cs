using Application.MassTransit.GetActiveCartByUserId;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MassTransit.UpdateCartStatus
{
    public class UpdateCartStatusRequest
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public CartStatus NewStatus { get; set; }
    }

}
