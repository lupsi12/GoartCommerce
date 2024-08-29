using MediatR;
using Core.Repositories;
using Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feature.Products.Commands.ReduceProductStock
{
    public class ReduceProductStockCommandHandler : IRequestHandler<ReduceProductStockCommandRequest, ReduceProductStockCommandResponse>
    {
        private readonly IWriteRepository<Product> _productWriteRepository;
        private readonly IReadRepository<Product> _productReadRepository;

        public ReduceProductStockCommandHandler(IWriteRepository<Product> productWriteRepository, IReadRepository<Product> productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<ReduceProductStockCommandResponse> Handle(ReduceProductStockCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetAsync(p => p.Id == request.ProductId);

            if (product == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            if (product.Stock < request.QuantityToReduce)
            {
                throw new InvalidOperationException("Not enough stock to reduce.");
            }

            product.Stock -= request.QuantityToReduce;

            await _productWriteRepository.UpdateAsync(product);
            await _productWriteRepository.SaveAsync();

            return new ReduceProductStockCommandResponse
            {
                ProductId = product.Id,
                RemainingStock = product.Stock,
            };
        }
    }
}
