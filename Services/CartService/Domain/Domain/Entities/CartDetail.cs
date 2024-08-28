using System;
using Core.Shared.EntityBase;

namespace Domain.Entities
{
    public class CartDetail : IEntityBase
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; } 
        public int Quantity { get; set; } 
        public decimal PricePerUnit { get; set; }
        public decimal Subtotal { get; set; }
        //public decimal Subtotal => Quantity * PricePerUnit; 

        public Cart Cart { get; set; } 



    }
}
