using Core.Shared.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; } 
        public string Description { get; set; } 
    }
}
