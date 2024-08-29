using System;
using Core.Shared.EntityBase;

namespace Domain.Entities
{
    public class Order : EntityBase
    {
        public int Id { get; set; } 
        public int UserId { get; set; } 
        public int CartId { get; set; }
        public OrderStatus Status { get; set; } 
        //total price
        public decimal TotalPrice { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
