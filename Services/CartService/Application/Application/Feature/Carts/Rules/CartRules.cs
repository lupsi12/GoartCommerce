using Domain.Entities;
using Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Application.Features.Carts.Rules
{
    public class CartRules
    {
        private readonly IReadRepository<Cart> _cartReadRepository;
        private readonly IWriteRepository<Cart> _cartWriteRepository;

        public CartRules(
            IReadRepository<Cart> cartReadRepository,
            IWriteRepository<Cart> cartWriteRepository
           )
        {
            _cartReadRepository = cartReadRepository;
            _cartWriteRepository = cartWriteRepository;
        }

        public void ValidateCartStatus(Cart cart)
        {
            if (cart.Status == CartStatus.Ordered || cart.Status == CartStatus.Cancelled)
            {
                throw new InvalidOperationException("Cannot modify a cart that has already been ordered or cancelled.");
            }
        }

        public void ValidateCartOwnership(int userId, Cart cart)
        {
            if (cart.UserId != userId)
            {
                throw new UnauthorizedAccessException("User does not have permission to modify this cart.");
            }
        }

  

        public void ValidateCartTotalPrice(Cart cart)
        {
            decimal calculatedTotal = cart.CartDetails.Sum(cd => cd.Subtotal);

            if (calculatedTotal != cart.TotalPrice)
            {
                throw new InvalidOperationException("Cart total price does not match the sum of cart detail subtotals.");
            }
        }

        public void EnsureCartIsActive(Cart cart)
        {
            if (cart.Status != CartStatus.Active)
            {
                throw new InvalidOperationException("Only active carts can be modified.");
            }
        }
    }
}
