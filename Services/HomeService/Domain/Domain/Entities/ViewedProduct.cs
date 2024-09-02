using System;
using System.Collections.Generic;
using Core.Shared.EntityBase;

namespace Domain.Entities
{
    public class ViewedProduct : EntityBase
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime ViewedDate { get; set; }
    }
}