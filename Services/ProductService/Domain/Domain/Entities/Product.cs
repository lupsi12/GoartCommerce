﻿using Core.Shared.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; } 
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; } 
        public int UserId { get; set; } //suplier ID
        public int CategoryId { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
