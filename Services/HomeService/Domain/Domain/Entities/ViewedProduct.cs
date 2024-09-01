using System;
using System.Collections.Generic;
using Core.Shared.EntityBase;

namespace Domain.Entities
{
    public class ViewedProduct : EntityMongoBase
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime ViewedDate { get; set; }
    }
}