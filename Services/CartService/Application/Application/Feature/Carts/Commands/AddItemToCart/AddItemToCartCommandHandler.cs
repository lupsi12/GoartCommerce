﻿using Application.MassTransit.GetProductById;
using Core.Repositories;
using Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Carts.Commands.AddItemToCart
{
    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommandRequest, AddItemToCartCommandResponse>
    {
        private readonly IReadRepository<Cart> _cartReadRepository;
        private readonly IWriteRepository<Cart> _cartWriteRepository;
        private readonly IWriteRepository<CartDetail> _cartDetailWriteRepository;
        private readonly ProductApiClient _productApiClient;
        private readonly IRequestClient<GetProductByIdRequest> _productClient;


        public AddItemToCartCommandHandler(
            IReadRepository<Cart> cartReadRepository,
            IWriteRepository<Cart> cartWriteRepository,
            IWriteRepository<CartDetail> cartDetailWriteRepository,
            ProductApiClient productApiClient,
               IRequestClient<GetProductByIdRequest> productClient)
        {
            _cartReadRepository = cartReadRepository;
            _cartWriteRepository = cartWriteRepository;
            _cartDetailWriteRepository = cartDetailWriteRepository;
            _productApiClient = productApiClient;
            _productClient = productClient;
        }

        public async Task<AddItemToCartCommandResponse> Handle(AddItemToCartCommandRequest request, CancellationToken cancellationToken)
        {
            //http call
            //var product = await _productApiClient.GetProductByIdAsync(request.ProductId);
            //mass transit
            var response = await _productClient.GetResponse<GetProductByIdResponse>(new GetProductByIdRequest { ProductId = request.ProductId });
            var product = response.Message;



            if (product == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            if (product.Stock < request.Quantity)
            {
                throw new InvalidOperationException("Insufficient stock available.");
            }

            var cart = await _cartReadRepository
                .GetAsync(c => c.UserId == request.UserId && c.Status == CartStatus.Active,
                         include: c => c.Include(cart => cart.CartDetails));

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = request.UserId,
                    Status = CartStatus.Active,
                    TotalPrice = 0
                };
                await _cartWriteRepository.AddAsync(cart);
                await _cartWriteRepository.SaveAsync();
            }

            var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.ProductId == request.ProductId);
            if (cartDetail == null)
            {
                cartDetail = new CartDetail
                {
                    CartId = cart.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    PricePerUnit = product.Price,
                    Subtotal = request.Quantity * product.Price
                };

                cart.CartDetails.Add(cartDetail);
                await _cartDetailWriteRepository.AddAsync(cartDetail);
            }
            else
            {
                cartDetail.Quantity += request.Quantity;

                if (cartDetail.Quantity > product.Stock)
                {
                    throw new InvalidOperationException("Insufficient stock available for the updated quantity.");
                }

                cartDetail.Subtotal = cartDetail.Quantity * cartDetail.PricePerUnit;
                await _cartDetailWriteRepository.UpdateAsync(cartDetail);
            }

            cart.TotalPrice = cart.CartDetails.Sum(cd => cd.Subtotal);
            await _cartWriteRepository.UpdateAsync(cart);
            await _cartDetailWriteRepository.SaveAsync();

            return new AddItemToCartCommandResponse
            {
                CartId = cart.Id,
                UserId = request.UserId,
                ProductId = request.ProductId,
                Quantity = cartDetail.Quantity,
                TotalPrice = cart.TotalPrice
            };
        }
    }
}
