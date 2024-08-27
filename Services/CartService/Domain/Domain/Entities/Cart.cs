using System;
using System.Collections.Generic;
using Core.Shared.EntityBase;

namespace Domain.Entities
{
    public class Cart : EntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public CartStatus Status { get; set; }

        //public decimal TotalPrice { get; set; } 
        public decimal TotalPrice => CartDetails.Sum(cd => cd.Subtotal);
        public ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
    }

    public enum CartStatus
    {
        Active,
        Inactive,
        Ordered,
        Pending,
        Abandoned,
        Cancelled
    }
}
