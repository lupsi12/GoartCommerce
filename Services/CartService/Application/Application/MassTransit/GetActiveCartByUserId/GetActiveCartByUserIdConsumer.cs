using Core.Repositories;
using Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MassTransit.GetActiveCartByUserId
{

    public class GetActiveCartByUserIdConsumer : IConsumer<GetActiveCartByUserIdRequest>
    {
        private readonly IReadRepository<Cart> _cartReadRepository;

        public GetActiveCartByUserIdConsumer(IReadRepository<Cart> cartReadRepository)
        {
            _cartReadRepository = cartReadRepository;
        }

        public async Task Consume(ConsumeContext<GetActiveCartByUserIdRequest> context)
        {
            var request = context.Message;

            var cart = await _cartReadRepository.GetAsync(
                c => c.UserId == request.UserId && c.Status == CartStatus.Active,
                include: c => c.Include(cart => cart.CartDetails));

            if (cart == null)
            {
                await context.RespondAsync<GetActiveCartByUserIdResponse>(null);
                return;
            }

            var response = new GetActiveCartByUserIdResponse
            {
                CartId = cart.Id,
                UserId = cart.UserId,
                Status = cart.Status,
                TotalPrice = cart.TotalPrice,
                CartItems = cart.CartDetails.Select(cd => new CartItemDto
                {
                    ProductId = cd.ProductId,
                    Quantity = cd.Quantity
                }).ToList()
            };

            await context.RespondAsync(response);
        }
    }
}