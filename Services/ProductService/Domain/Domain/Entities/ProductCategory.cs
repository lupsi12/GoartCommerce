using Core.Shared.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductCategory : IEntityBase
    { 
        public int ProductId { get; set; } 
        public virtual Product Product { get; set; } 

        public int CategoryId { get; set; } 
        public virtual Category Category { get; set; } 
    }
}
