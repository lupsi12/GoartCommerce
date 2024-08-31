using Core.Repositories;
using Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MassTransit.GetProductById
{
    public class GetProductByIdConsumer : IConsumer<GetProductByIdRequest>
    {
        private readonly IReadRepository<Product> _productReadRepository;

        public GetProductByIdConsumer(IReadRepository<Product> productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task Consume(ConsumeContext<GetProductByIdRequest> context)
        {
            var request = context.Message;
            var product = await _productReadRepository.GetAsync(
                predicate: p => p.Id == request.ProductId,
                include: q => q.Include(p => p.ProductCategories),
                enableTracking: false);

            if (product == null)
            {
                await context.RespondAsync<GetProductByIdResponse>(null);
                return;
            }

            var response = new GetProductByIdResponse
            {
                ProductId = product.Id,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Price = product.Price,
                UserId = product.UserId,
                CategoryIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList(),
                CreatedDate = product.CreatedDate
            };

            await context.RespondAsync(response);
        }
    }
}
